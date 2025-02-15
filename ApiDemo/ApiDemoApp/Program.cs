using DataLibrary.Data;
using DataLibrary.Db;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//[+]Custom Services
builder.Services.AddSingleton(new ConnectionStringData
{
    SqlConnectionName = "Default"
});
builder.Services.AddSingleton<IDataAccess, SqlDb>();
builder.Services.AddSingleton<IOrderData, OrderData>();
builder.Services.AddSingleton<IFoodData, FoodData>();
//[-]Custom Services



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
