using Consumers.AspNetCore.Attributes;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Consumers.AspNetCore.Extensions;

public static class EndpointConventionBuilderExtensions
{
    public static IServiceCollection AddConsumers(this IServiceCollection services, Action<ConsumersOptions> configure)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configure);

        services.Configure(configure);
        services.AddSingleton<MatcherPolicy, ConsumerMatcherPolicy>();
        return services;
    }

    public static TBuilder RequireConsumers<TBuilder>(this TBuilder builder, params string[] consumers) where TBuilder : IEndpointConventionBuilder
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(consumers);

        builder.Add(endpointBuilder =>
        {
            endpointBuilder.Metadata.Add(new ConsumersAttribute(consumers));
        });
        return builder;
    }
}