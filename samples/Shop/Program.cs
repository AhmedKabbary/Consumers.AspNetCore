using Consumers.AspNetCore.Extensions;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.AddConsumers(options => options.HeaderName = "X-Consumer");

var app = builder.Build();

app.MapGet("/hello", () => "Hello Vendor!")
    .RequireConsumers(Shop.Consumers.Vendor);

app.MapGet("/hello", () => "Hello Customer!")
    .RequireConsumers(Shop.Consumers.Customer);

app.Run();