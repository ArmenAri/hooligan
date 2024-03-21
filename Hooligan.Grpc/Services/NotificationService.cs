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
    }
}
