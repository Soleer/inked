﻿namespace Inked.Catalog.API.IntegrationEvents.Events;

// Integration Events notes: 
// An Event is “something that has happened in the past”, therefore its name has to be past tense
// An Integration Event is an event that can cause side effects to other microservices, Bounded-Contexts or external systems.
public record ProductPriceChangedIntegrationEvent(int ProductId, decimal NewPrice, decimal OldPrice) : IntegrationEvent;
