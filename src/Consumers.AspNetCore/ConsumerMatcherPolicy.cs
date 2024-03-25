using Consumers.AspNetCore.Attributes;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Matching;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Consumers.AspNetCore;

public class ConsumerMatcherPolicy : MatcherPolicy, IEndpointSelectorPolicy
{
    public override int Order => 0;

    public bool AppliesToEndpoints(IReadOnlyList<Endpoint> endpoints)
    {
        ArgumentNullException.ThrowIfNull(endpoints);

        return endpoints.Any(e => e.Metadata.GetMetadata<ConsumersAttribute>() is not null);
    }

    public Task ApplyAsync(HttpContext httpContext, CandidateSet candidates)
    {
        ArgumentNullException.ThrowIfNull(httpContext);
        ArgumentNullException.ThrowIfNull(candidates);

        IOptions<ConsumersOptions> options = httpContext.RequestServices.GetRequiredService<IOptions<ConsumersOptions>>();
        String? consumer = httpContext.Request.Headers[options.Value.HeaderName].FirstOrDefault();
        if (consumer is null) return Task.CompletedTask;

        for (var i = 0; i < candidates.Count; i++)
        {
            Endpoint endpoint = candidates[i].Endpoint;
            ConsumersAttribute? consumersAttribute = endpoint.Metadata.GetMetadata<ConsumersAttribute>();

            if (consumersAttribute is not null)
            {
                if (!consumersAttribute.Consumers.Contains(consumer))
                {
                    candidates.SetValidity(i, false);
                }
            }
        }

        return Task.CompletedTask;
    }
}