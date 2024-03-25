using Consumers.AspNetCore.Attributes;

using Microsoft.AspNetCore.Builder;

namespace Consumers.AspNetCore.Extensions;

public static class EndpointConventionBuilderExtensions
{
    public static TBuilder RequireConsumers<TBuilder>(this TBuilder builder, params String[] consumers)
        where TBuilder : IEndpointConventionBuilder
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(consumers);

        builder.Add(endpointBuilder => endpointBuilder.Metadata.Add(new ConsumersAttribute(consumers)));
        return builder;
    }
}