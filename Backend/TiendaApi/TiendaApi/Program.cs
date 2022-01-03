using TiendaApi.Models;
using TiendaApi.services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<ItemStoreDatabaseSettings>(
builder.Configuration.GetSection("ItemStoreDatabase"));
builder.Services.AddSingleton<ItemsService>();


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
