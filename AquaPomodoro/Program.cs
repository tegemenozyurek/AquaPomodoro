using Microsoft.EntityFrameworkCore;
using AquaPomodoro.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add Entity Framework
builder.Services.AddDbContext<AquaPomodoroDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();