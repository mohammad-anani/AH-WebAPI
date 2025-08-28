using Autofac;
using Autofac.Extensions.DependencyInjection;
using Serilog;
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

builder.Host.UseSerilog();

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