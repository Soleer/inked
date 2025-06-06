﻿namespace Inked.Ordering.API.Application.DomainEventHandlers;

public class
    UpdateOrderWhenBuyerAndPaymentMethodVerifiedDomainEventHandler : INotificationHandler<
    BuyerAndPaymentMethodVerifiedDomainEvent>
{
    private readonly ILogger _logger;
    private readonly IOrderRepository _orderRepository;

    public UpdateOrderWhenBuyerAndPaymentMethodVerifiedDomainEventHandler(
        IOrderRepository orderRepository,
        ILogger<UpdateOrderWhenBuyerAndPaymentMethodVerifiedDomainEventHandler> logger)
    {
        _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    // Domain Logic comment:
    // When the Buyer and Buyer's payment method have been created or verified that they existed, 
    // then we can update the original Order with the BuyerId and PaymentId (foreign keys)
    public async Task Handle(BuyerAndPaymentMethodVerifiedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        var orderToUpdate = await _orderRepository.GetAsync(domainEvent.OrderId);
        orderToUpdate.SetPaymentMethodVerified(domainEvent.Buyer.Id, domainEvent.Payment.Id);
        OrderingApiTrace.LogOrderPaymentMethodUpdated(_logger, domainEvent.OrderId, nameof(domainEvent.Payment),
            domainEvent.Payment.Id);
    }
}
