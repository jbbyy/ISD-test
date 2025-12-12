using System.Text.Json;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<CustomerMemory>(provider =>
{
    // Load customers from JSON file
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
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
