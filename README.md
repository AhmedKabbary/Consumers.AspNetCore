# Consumers.AspNetCore

Route ASP.NET Core Web API endpoints based on consumer type. This is particularly useful when you need to perform different logic or return different responses to different API consumers using the same route.

## Key Features

- Routes requests based on consumer type passed in a custom header.
- Provides a clean and declarative way to separate endpoints with the same method/path combination.

## Usage

### 1. Configure the services

```csharp
builder.Services.AddConsumers(options => options.HeaderName = "X-Consumer");
```

### 2. Annotate the endpoints

#### 2.1. Controllers

```csharp
[HttpGet]
[Consumers("Vendor")]
public IActionResult GetProductsForVendor()
{
    return Ok("This is the list of products for vendor");
}

[HttpGet]
[Consumers("Customer")]
public IActionResult GetProductsForCustomer()
{
    return Ok("This is the list of products for customer");
}
```

#### 2.2. Minimal APIs

```csharp
app.MapGet("/hello", () => "Hello Vendor!")
    .RequireConsumers("Vendor");

app.MapGet("/hello", () => "Hello Customer!")
    .RequireConsumers("Customer");
```

## Known issues

### OpenAPI (Swagger)

OpenAPI doesn't support multiple endpoints with the same path and method combination in the same document.

You can create multiple swagger documents for each API consumer

```csharp
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("vendors", new OpenApiInfo { Title = "E-commerce Vendors API", Version = "v1" });
    options.SwaggerDoc("customers", new OpenApiInfo { Title = "E-commerce Customers API", Version = "v1" });
});
```

```csharp
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/vendors/swagger.json", "E-commerce Vendors API");
        options.SwaggerEndpoint("/swagger/customers/swagger.json", "E-commerce Customers API");
    });
}
```

And annotate the controller actions with `ApiExplorerSettings` attribute

```csharp
[HttpGet]
[Consumers("Vendor")]
[ApiExplorerSettings(GroupName = "vendors")] // <-- Add this line
public IActionResult GetProductsForVendor()
{
    return Ok("This is the list of products for vendor");
}

[HttpGet]
[Consumers("Customer")]
[ApiExplorerSettings(GroupName = "customers")] // <-- Add this line
public IActionResult GetProdcutsForCustomer()
{
    return Ok("This is the list of products for customer");
}
```

Or annotate the minimal API endpoints with `WithGroupName` method

```csharp
app.MapGet("/hello", () => "Hello Vendor!")
    .RequireConsumers("Vendor")
    .WithGroupName("vendors"); // <-- Add this line

app.MapGet("/hello", () => "Hello Customer!")
    .RequireConsumers("Customer")
    .WithGroupName("customers"); // <-- Add this line
```

Then create an operation filter to add the `X-Consumer` header to the swagger UI

```csharp
public class AddConsumerHeadersOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "X-Consumer",
            In = ParameterLocation.Header,
            Required = true,
            Examples = new Dictionary<String, OpenApiExample>
            {
                { "Vendor", new OpenApiExample { Value = new OpenApiString("Vendor") } },
                { "Customer", new OpenApiExample { Value = new OpenApiString("Customer") } },
            }
        });
    }
}
```

then register it in the swagger configuration

```csharp
builder.Services.AddSwaggerGen(options =>
{
    options.OperationFilter<AddConsumerHeadersOperationFilter>(); // <-- Add this line
    options.SwaggerDoc("vendors", new OpenApiInfo { Title = "E-commerce Vendors API", Version = "v1" });
    options.SwaggerDoc("customers", new OpenApiInfo { Title = "E-commerce Customers API", Version = "v1" });
});
```
