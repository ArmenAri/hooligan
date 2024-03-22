using Hooligan.Messages;
using MassTransit;
using NotificationService = Hooligan.Grpc.Services.NotificationService;

namespace Hooligan.Grpc.Consumers;

public sealed class AssociationConsumer(
    ILogger<AssociationConsumer> logger,
    NotificationService service) : IConsumer<NewAssociation>
{
    public Task Consume(ConsumeContext<NewAssociation> context)
    {
        service.SendNotification(
            $"New association was discovered : {context.Message.name}"
        );

        return Task.CompletedTask;
    }
}