using Cleemy.Context;
using Cleemy.Models;
using Cleemy.Models.Object;
using Cleemy.Services;
using Cleemy.Services.Object;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Services
builder.Services.AddScoped<IUserExpenseService, UserExpenseService>();

//Models
builder.Services.AddScoped<IUserExpenseModel, UserExpenseModel>();

builder.Services.AddDbContext<CleemyBDContext>(
    options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("CleemyDB"));
    });

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
