﻿namespace Inked.Ordering.API.Application.Commands;

// DDD and CQRS patterns comment: Note that it is recommended to implement immutable Commands
// In this case, its immutability is achieved by having all the setters as private
// plus only being able to update the data just once, when creating the object through its constructor.
// References on Immutable Commands:  
// http://cqrs.nu/Faq
// https://docs.spine3.org/motivation/immutability.html 
// http://blog.gauffin.org/2012/06/griffin-container-introducing-command-support/
// https://docs.microsoft.com/dotnet/csharp/programming-guide/classes-and-structs/how-to-implement-a-lightweight-class-with-auto-implemented-properties

[DataContract]
public class CreateOrderCommand
    : IRequest<bool>
{
    [DataMember] private readonly List<OrderItemDTO> _orderItems;

    public CreateOrderCommand()
    {
        _orderItems = new List<OrderItemDTO>();
    }

    public CreateOrderCommand(List<BasketItem> basketItems, string userId, string userName, string city, string street,
        string state, string country, string zipcode,
        string cardNumber, string cardHolderName, DateTime cardExpiration,
        string cardSecurityNumber, int cardTypeId)
    {
        _orderItems = basketItems.ToOrderItemsDTO().ToList();
        UserId = userId;
        UserName = userName;
        City = city;
        Street = street;
        State = state;
        Country = country;
        ZipCode = zipcode;
        CardNumber = cardNumber;
        CardHolderName = cardHolderName;
        CardExpiration = cardExpiration;
        CardSecurityNumber = cardSecurityNumber;
        CardTypeId = cardTypeId;
    }

    [DataMember] public string UserId { get; private set; }

    [DataMember] public string UserName { get; private set; }

    [DataMember] public string City { get; private set; }

    [DataMember] public string Street { get; private set; }

    [DataMember] public string State { get; private set; }

    [DataMember] public string Country { get; private set; }

    [DataMember] public string ZipCode { get; private set; }

    [DataMember] public string CardNumber { get; private set; }

    [DataMember] public string CardHolderName { get; private set; }

    [DataMember] public DateTime CardExpiration { get; private set; }

    [DataMember] public string CardSecurityNumber { get; private set; }

    [DataMember] public int CardTypeId { get; private set; }

    [DataMember] public IEnumerable<OrderItemDTO> OrderItems => _orderItems;
}
