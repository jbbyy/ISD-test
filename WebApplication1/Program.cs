using System.Text.Json;
using Serilog;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Service;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .Enrich.FromLogContext()
    .WriteTo.Console() // Docker / Render logs
    .WriteTo.File(
        path: "Logs/log-.txt",
        rollingInterval: RollingInterval.Day
    )
    .CreateLogger();

builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddSingleton<CustomerMemory>(provider =>
{
    var jsonPath = Path.Combine(builder.Environment.ContentRootPath, "Data", "CustomerDatabase.json");
    var json = File.ReadAllText(jsonPath);
    var customers = JsonSerializer.Deserialize<List<CustomerDto>>(json);

    return new CustomerMemory
    {
        Customers = customers ?? new List<CustomerDto>()
    };
});

builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var port = Environment.GetEnvironmentVariable("PORT") ?? "10000";
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.MapGet("/", () => "WebApplication1 API is running");

try
{
    Log.Information("Starting WebApplication1");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application failed to start");
}
finally
{
    Log.CloseAndFlush();
}