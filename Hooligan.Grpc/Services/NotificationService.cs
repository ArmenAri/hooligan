using Grpc.Core;
using ProtosGrpc;

namespace Hooligan.Infrastructure.GrpcService
{
    public class NotificationService : Notification.NotificationBase
    {
        private readonly ILogger<NotificationService> _logger;
        public NotificationService(ILogger<NotificationService> logger)
        {
            _logger = logger;
        }

        public override Task<NotificationResponse> SendNotification(NotificationRequest request, ServerCallContext context)
        {
            return Task.FromResult(new NotificationResponse
            {
                Message = "This a test answer !"
            });
        }

        public async override Task Subscribe(SubscribeRequest request, IServerStreamWriter<NotificationStreamResponse> responseStream, ServerCallContext context)
        {
            while (!context.CancellationToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(5)); // Attendre 15 secondes

                // Envoyer une notification au client
                await responseStream.WriteAsync(new NotificationStreamResponse { Message = "Nouvelle notification" });
            }
        }
    }
}
