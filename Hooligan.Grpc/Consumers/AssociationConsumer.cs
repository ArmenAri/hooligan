using Hooligan.Common.Messages;
using MassTransit;
using NotificationService = Hooligan.Grpc.Services.NotificationService;

namespace Hooligan.Grpc.Consumers;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class AssociationConsumer(NotificationService service) : IConsumer<NewAssociation>
{
    public Task Consume(ConsumeContext<NewAssociation> context)
    {
        service.SendNotification(
            $"New association was discovered : {context.Message.Name}"
        );

        return Task.CompletedTask;
    }
}