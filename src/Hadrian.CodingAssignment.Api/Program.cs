using System.Text.Json.Serialization;
using Hadrian.CodingAssignment.Infrastructure.DI;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(config =>
{
    config.SwaggerDoc("v1", new() { Title = "Hadrian Coding Assignment API", Version = "v1" });
});

builder.Services.AddHadrianDbStorage(builder.Configuration);
builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint($"v1/swagger.json", "Hadrian Coding Assignment API - v1");
    });
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();

namespace Hadrian.CodingAssignment.Api
{
#pragma warning disable 1591
    public sealed partial class Program
#pragma warning restore 1591
    {
        // Expose the Program class for use with WebApplicationFactory<T> in tests
    }
}
