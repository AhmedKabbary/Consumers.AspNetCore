using Consumers.AspNetCore.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddConsumers(options =>
{
    options.HeaderName = "X-Consumer";
    options.Consumers.Add(Ecommerce.Consumers.Vendor);
    options.Consumers.Add(Ecommerce.Consumers.Customer);
});

var app = builder.Build();

app.MapGet("/hello", () => "Hello Vendor!")
    .RequireConsumers(Ecommerce.Consumers.Vendor);

app.MapGet("/hello", () => "Hello Customer!")
    .RequireConsumers(Ecommerce.Consumers.Customer);

app.Run();