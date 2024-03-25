using Consumers.AspNetCore.Extensions;

using Library;

using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateSlimBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddConsumers(options => options.HeaderName = "X-Consumer");

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.OperationFilter<AddConsumerHeadersOperationFilter>();
    options.SwaggerDoc("users", new OpenApiInfo { Title = "Library Users API", Version = "v1" });
    options.SwaggerDoc("librarians", new OpenApiInfo { Title = "Library Librarians API", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/users/swagger.json", "Library Users API");
        options.SwaggerEndpoint("/swagger/librarians/swagger.json", "Library Librarians API");
    });
}

app.MapControllers();

app.Run();