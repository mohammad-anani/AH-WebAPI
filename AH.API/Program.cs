using Autofac;
using Autofac.Extensions.DependencyInjection;
using Serilog;
using System.Reflection;
using AH.API.Routing;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

ConfigHelper.Initialize(builder.Configuration);
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidationFilter>();
    // Apply slugify convention so [controller] becomes dashed lowercase
    options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
});// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    //Repositories Injection
    containerBuilder.RegisterAssemblyTypes(Assembly.Load("AH.Infrastructure"))
        .Where(t => t.Namespace == "AH.Infrastructure.Repositories")
        .AsImplementedInterfaces()
        .InstancePerLifetimeScope();

    //Services Injection
    containerBuilder.RegisterAssemblyTypes(Assembly.Load("AH.Application"))
    .Where(t => t.Namespace == "AH.Application.Services")
    .AsImplementedInterfaces()
    .InstancePerLifetimeScope();

    //IServices Injection
    containerBuilder.RegisterAssemblyTypes(Assembly.Load("AH.Application"))
    .Where(t => t.Namespace == "AH.Application.IServices")
    .AsImplementedInterfaces()
    .InstancePerLifetimeScope();

    //IRepositories Injection
    containerBuilder.RegisterAssemblyTypes(Assembly.Load("AH.Infrastructure"))
    .Where(t => t.Namespace == "AH.Infrastructure.IRepositories")
    .AsImplementedInterfaces()
    .InstancePerLifetimeScope();

    //Configuration Injection
    containerBuilder.Register(ctx => builder.Configuration)
       .As<IConfiguration>()
       .SingleInstance();
});

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration) // load from appsettings.json
    .Enrich.FromLogContext()
    .WriteTo.Console() // at least write to console
    .CreateLogger();

builder.Host.UseSerilog(Log.Logger);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHsts();
app.UseHttpsRedirection();

app.UseSerilogRequestLogging();

app.UseAuthorization();

app.MapControllers();
app.Run();