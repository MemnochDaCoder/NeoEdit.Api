using Microsoft.EntityFrameworkCore;
using NeoEdit.Api.Data.Repositories;
using NeoEdit.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Register the controllers as services in the application.
builder.Services.AddControllers();

// Add the endpoints API explorer which is necessary for Swagger.
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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

// If using Entity Framework, setup the DataContext
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Enable middleware to serve generated Swagger as a JSON endpoint.
    app.UseSwagger();
    // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
    // specifying the Swagger JSON endpoint.
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "NeoEdit API V1"));
}

// Enable middleware to redirect HTTP requests to HTTPS.
app.UseHttpsRedirection();

// Use authorization middleware to secure the app.
app.UseAuthorization();

// Map controllers to routes.
app.MapControllers();

// Run the application.
app.Run();
