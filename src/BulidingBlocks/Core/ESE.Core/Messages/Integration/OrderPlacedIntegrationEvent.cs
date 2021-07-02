using System;

namespace ESE.Core.Messages.Integration
{
    public class OrderPlacedIntegrationEvent : IntegrationEvent
    {
        public Guid CustomerId { get; set; }

        public OrderPlacedIntegrationEvent(Guid customerId)
        {
            CustomerId = customerId;
        }
    }
}
