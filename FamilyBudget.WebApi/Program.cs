using Autofac.Core;
using FamilyBudget.Api.BLL;
using FamilyBudget.Api.DAL;
using FamilyBudget.Api.DAL.Context;
using FamilyBudget.Api.Interface;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//builder.Services.AddApiVersioning(options =>
//{
//    options.ReportApiVersions = true;
//    options.AssumeDefaultVersionWhenUnspecified = true;
//    options.DefaultApiVersion = new ApiVersion(1, 0);
//    options.ApiVersionReader = new HeaderApiVersionReader("x-api-version");
//});


//builder.Services.AddCors(options =>
//{
//    options.AddPolicy(_allowAllOriginsPolicy, policyBuilder =>
//        {
//      policyBuilder.AllowAnyOrigin();
//      policyBuilder.AllowAnyMethod();
//      policyBuilder.AllowAnyHeader();
//  });
//});

//register out 

builder.Services.AddSingleton<FamilyBudgetDbContext>();
builder.Services.AddScoped<IFamilyBudgetRepository, FamilyBudgetRepository>();
builder.Services.AddScoped<IFamilyBudgetService, FamilyBudgetService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1.0", new OpenApiInfo
        {
            Title = "FamilyBudget API",
            Version = "v1.0"
        });
    });



var app = builder.Build();

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
