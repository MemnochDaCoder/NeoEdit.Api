using Microsoft.EntityFrameworkCore;
using NeoEdit.Api.Data.Repositories;
using NeoEdit.Api.Eventing;
using NeoEdit.Api.Services;
using RabbitMQ.Client;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Register the controllers as services in the application.
builder.Services.AddControllers();

// Add the endpoints API explorer which is necessary for Swagger.
builder.Services.AddEndpointsApiExplorer();

// Setup the DataContext for PostgreSQL
builder.Services.AddDbContext<DataContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))); // Changed from UseSqlServer to UseNpgsql

// Setup RabbitMQ connection
builder.Services.AddSingleton(sp =>
{
    var factory = new ConnectionFactory()
    {
        HostName = "localhost", // Customize as needed, for Docker use the service name defined in docker-compose.yml
        UserName = builder.Configuration["RabbitMQ:UserName"], // Recommended to use configurations
        Password = builder.Configuration["RabbitMQ:Password"]
    };
    return factory.CreateConnection();
});
builder.Services.AddSingleton<RabbitMQClient>();

// Add Swagger generator to provide a Swagger JSON endpoint.
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "NeoEdit API",
        Version = "v1",
        Description = "An API for managing documents and tasks in NeoEdit.",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Support",
            Email = "support@example.com",
            Url = new Uri("https://support.example.com")
        }
    });
});

// Register the document repository and service
builder.Services.AddScoped<IDocumentRepository, DocumentRepository>();
builder.Services.AddScoped<IDocumentService, DocumentService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "NeoEdit API V1"));
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
