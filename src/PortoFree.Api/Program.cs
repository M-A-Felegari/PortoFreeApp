using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using PortoFree.Application.Extensions;
using PortoFree.Application.Interfaces.Seeders;
using PortoFree.Domain.Constants;
using PortoFree.Domain.Entities;
using PortoFree.Infrastructure.Extensions;
using PortoFree.Infrastructure.Persistence;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAuthentication();
builder.Services.AddAuthorization(opt =>
{
    opt.AddPolicy(UserRoles.Owner, policy =>
    {
        policy.RequireRole(UserRoles.Owner);
    });
    opt.AddPolicy(UserRoles.Admin, policy =>
    {
        policy.RequireRole(UserRoles.Admin);
    });
    opt.AddPolicy(UserRoles.User, policy =>
    {
        policy.RequireRole(UserRoles.User);
    });
});

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme()
    {
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        Description = "just enter token, automatically Bearer added to first of it"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme()
            {
                Reference = new OpenApiReference()
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "bearerAuth"
                }
            },
            []
        }
    });
});

builder.Host.UseSerilog((context, configuration) =>
{
    configuration
        .ReadFrom.Configuration(context.Configuration);
});

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
