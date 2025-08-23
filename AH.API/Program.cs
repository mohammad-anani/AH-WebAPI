using AH.Application.IRepositories;
using AH.Application.IServices;
using AH.Application.Services;
using AH.Infrastructure.Repositories;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

ConfigHelper.Initialize(builder.Configuration);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    //Repositories Injection
    containerBuilder.RegisterAssemblyTypes(Assembly.Load("AH.Infrastructure"))
        .Where(t => t.Namespace=="AH.Infrastructure.Repositories") 
        .AsImplementedInterfaces()               
        .InstancePerLifetimeScope();

    //Services Injection
    containerBuilder.RegisterAssemblyTypes(Assembly.Load("AH.Application"))
    .Where(t => t.Namespace == "AH.Application.Services")
    .AsImplementedInterfaces()
    .InstancePerLifetimeScope();

    //Configuration Injection
    containerBuilder.Register(ctx => builder.Configuration)
       .As<IConfiguration>()
       .SingleInstance();


});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHsts();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();