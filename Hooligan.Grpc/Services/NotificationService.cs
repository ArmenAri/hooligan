using System.Collections.Concurrent;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using HooliganNotification;

namespace Hooligan.Grpc.Services;

public sealed class NotificationService
    : HooliganNotification.NotificationService.NotificationServiceBase
{
    private readonly ConcurrentDictionary<Guid, IServerStreamWriter<Notification>> _subscribers = new();
    private readonly ConcurrentQueue<string> _notifications = [];

    public override async Task Subscribe(Empty request,
        IServerStreamWriter<Notification> responseStream, ServerCallContext context)
    {
        var clientId = Guid.NewGuid();
        _subscribers.TryAdd(clientId, responseStream);

        try
        {
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
        finally
        {
            _subscribers.TryRemove(clientId, out _);
        }
    }

    public void SendNotification(string message)
    {
        _notifications.Enqueue(message);
    }
}