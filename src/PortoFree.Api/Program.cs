using Microsoft.AspNetCore.Identity;
using PortoFree.Application.Interfaces.Seeding;
using PortoFree.Domain.Entities;
using PortoFree.Infrastructure.Extensions;
using PortoFree.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using var scope = app.Services.CreateScope();
var seedingService = scope.ServiceProvider.GetRequiredService<IOwnerSeeder>();
await seedingService.SeedAsync();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGroup("/identity").MapIdentityApi<User>();

app.UseAuthorization();

app.MapControllers();

app.Run();
