using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Consumers.AspNetCore.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddConsumers(this IServiceCollection services, Action<ConsumersOptions> configure)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configure);

        services.Configure(configure);
        services.AddSingleton<MatcherPolicy, ConsumerMatcherPolicy>();
        return services;
    }
}