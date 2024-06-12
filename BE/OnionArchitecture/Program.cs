
using Domain.Entities;
using Domain.ModelEmail;
using FluentValidation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using NLog.Extensions.Logging;
using OnionArchitecture.ConfigureDependencyInjection;
using OnionArchitecture.ConfigureJWT;
using OnionArchitecture.ConfigureMapper;
using OnionArchitecture.Middleware;
using Persistence.Configure.ConfigureDbContext;
using Persistence.Context;


var builder = WebApplication.CreateBuilder(args);
var myCors = "myCors";

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.RegisterDbContext(builder.Configuration);
builder.Services.AddRegisterDependencyInjection();
builder.Services.AddAutoMapper(typeof(RegisterMapper).Assembly);
builder.Services.RegisterTokenBear(builder.Configuration);
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myCors,
                      builder =>
                      {
                          builder.AllowAnyHeader();
                          builder.AllowAnyMethod();
                          builder.AllowAnyOrigin();
                          builder.WithOrigins();
                        
                      });
});
builder.Services.AddLogging(logging =>
{
    logging.AddNLog();
    logging.ClearProviders();
    logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Warning);
});
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Sample.WebApiRestful",
        Version = "v1",
        Description = "This is Swagger WebAPI Restful",

    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        In = ParameterLocation.Header,
        BearerFormat = "JWT",
        Scheme = "Bearer",
        Description = "Please input your token"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                        new string[] { }
        }
                });
});
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.Configure<EmailConfig>(builder.Configuration.GetSection("MailSettings"));


//builder.Services.AddNCacheDistributedCache(configuration =>
//{
//    configuration.CacheName = "MyNCache";
//    configuration.EnableLogs = true;
//    configuration.ExceptionsEnabled = true;
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandler>();
app.UseCors(myCors);
app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
