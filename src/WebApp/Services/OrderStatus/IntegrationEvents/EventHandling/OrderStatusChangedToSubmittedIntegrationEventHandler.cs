﻿using Inked.EventBus.Abstractions;

namespace Inked.WebApp.Services.OrderStatus.IntegrationEvents;

public class OrderStatusChangedToSubmittedIntegrationEventHandler(
    OrderStatusNotificationService orderStatusNotificationService,
    ILogger<OrderStatusChangedToSubmittedIntegrationEventHandler> logger)
    : IIntegrationEventHandler<OrderStatusChangedToSubmittedIntegrationEvent>
{
    public async Task Handle(OrderStatusChangedToSubmittedIntegrationEvent @event)
    {
        logger.LogInformation("Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", @event.Id,
            @event);
        await orderStatusNotificationService.NotifyOrderStatusChangedAsync(@event.BuyerIdentityGuid);
    }
}
