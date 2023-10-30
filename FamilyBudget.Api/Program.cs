using Autofac.Core;
using FamilyBudget.Api.BLL;
using FamilyBudget.Api.DAL;
using FamilyBudget.Api.DAL.Context;
using FamilyBudget.Api.DAL.Interface;
using FamilyBudget.Api.DAL.Repository;
using FamilyBudget.Api.Interface;
using FamilyBudget.Api.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Data;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
var configuration = builder.Configuration;

#region Services
builder.Services.AddSingleton<FamilyBudgetDbContext>();
builder.Services.AddScoped<IFamilyBudgetService, FamilyBudgetService>();
builder.Services.AddScoped<IFamilyBudgetRepository, FamilyBudgetRepository>();

//fooly me not scoped...
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
#endregion

#region Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1.0", new OpenApiInfo
        {
            Title = "FamilyBudget API",
            Version = "v1.0"
        });

        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
        });

        options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}
                    }
                });


    });
#endregion

#region Authentication
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true,
        ValidIssuer = configuration["Jwt:Issuer"],
        ValidAudience = configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
    };
});
#endregion

var app = builder.Build();
app.UseMiddleware<JWTMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1.0/swagger.yaml", "FamilyBudget API v1.0");
    options.RoutePrefix = "swagger";
    options.DocExpansion(DocExpansion.Full);
});
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
