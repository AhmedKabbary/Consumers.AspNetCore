using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace Library;

public class AddConsumerHeadersOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.Parameters.Add(
            new OpenApiParameter
            {
                Name = "X-Consumer",
                In = ParameterLocation.Header,
                Required = true,
                Examples = new Dictionary<String, OpenApiExample>
                {
                    { "user", new OpenApiExample { Value = new OpenApiString(Consumers.User) } },
                    { "librarian", new OpenApiExample { Value = new OpenApiString(Consumers.Librarian) } },
                }
            }
        );
    }
}