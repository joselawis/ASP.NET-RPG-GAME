using Combat.Application.Common.Interfaces;
using Combat.Domain.Common;
using MassTransit;

namespace Combat.Infrastructure.Messaging
{
    public class DomainEventDispatcher(IPublishEndpoint publishEndpoint) : IDomainEventDispatcher
    {
        private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

        public async Task DispatchAsync(DomainEvent domainEvent, CancellationToken cancellationToken = default)
        {
            // MassTransit handles serialization and publishing of the event in RabbitMQ
            await _publishEndpoint.Publish(domainEvent, cancellationToken);
        }
    }
}