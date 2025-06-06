﻿using System.Text.Json.Serialization;
using Inked.OrderProcessor.Events;

namespace Inked.OrderProcessor.Extensions;

public static class Extensions
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        builder.AddRabbitMqEventBus("eventbus")
            .ConfigureJsonOptions(options => options.TypeInfoResolverChain.Add(IntegrationEventContext.Default));

        builder.AddNpgsqlDataSource("orderingdb");

        builder.Services.AddOptions<BackgroundTaskOptions>()
            .BindConfiguration(nameof(BackgroundTaskOptions));

        builder.Services.AddHostedService<GracePeriodManagerService>();
    }
}

[JsonSerializable(typeof(GracePeriodConfirmedIntegrationEvent))]
internal partial class IntegrationEventContext : JsonSerializerContext
{
}
