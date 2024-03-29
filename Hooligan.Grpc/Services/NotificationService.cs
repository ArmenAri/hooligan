using System.Collections.Concurrent;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using HooliganNotification;

namespace Hooligan.Grpc.Services;

public sealed class NotificationService
    : HooliganNotification.NotificationService.NotificationServiceBase
{
    private readonly ConcurrentDictionary<string, IServerStreamWriter<Notification>> _subscribers = new();
    private readonly ConcurrentQueue<string> _notifications = [];

    public override async Task Subscribe(Empty request,
        IServerStreamWriter<Notification> responseStream, ServerCallContext context)
    {
        _subscribers.TryAdd(Guid.NewGuid().ToString(), responseStream);

        while (!context.CancellationToken.IsCancellationRequested)
        {
            if (!_notifications.TryDequeue(out var notification))
            {
                continue;
            }

            foreach (var stream in _subscribers.Values)
            {
                await stream.WriteAsync(new Notification { Message = notification });
            }
        }
    }

    public void SendNotification(string message)
    {
        _notifications.Enqueue(message);
    }
}