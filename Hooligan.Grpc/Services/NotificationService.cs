using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using HooliganNotification;

namespace Hooligan.Grpc.Services;

public sealed class NotificationService : HooliganNotification.NotificationService.NotificationServiceBase
{
    private readonly Queue<string> _notifications = [];

    public override async Task Subscribe(Empty request,
        IServerStreamWriter<Notification> responseStream, ServerCallContext context)
    {
        while (!context.CancellationToken.IsCancellationRequested)
        {
            var canDequeue = _notifications.TryDequeue(out var notification);

            if (!canDequeue || notification is null)
            {
                continue;
            }

            await responseStream.WriteAsync(new Notification { Message = notification }, context.CancellationToken);
        }
    }

    // Method to send a new notification
    public void SendNotification(string message)
    {
        _notifications.Enqueue(message);
    }
}