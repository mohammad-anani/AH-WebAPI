using AH.API.Routing;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using Serilog;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Use Autofac as the DI container instead of the default
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// Initialize configuration helper
ConfigHelper.Initialize(builder.Configuration);

// Add controllers and filters and configs
builder.Services.AddControllers(options =>
{
    // Add validation filter to handle model validation errors globally
    options.Filters.Add<ValidationFilter>();

    // Apply slugify convention so [controller] becomes dashed lowercase
    options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));

    // Create a policy that requires an authenticated user
    var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();

    // Add it as a global filter
    options.Filters.Add(new AuthorizeFilter(policy));
});// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Ioc Container Configuration
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

// Configure JSON serialization to use Newtonsoft.Json and keep property names as in C# (PascalCase)
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        // Keep PascalCase (property names as in C#)
        options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();

        // Optional: format JSON nicely
        options.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
    });

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration) // load from appsettings.json
    .Enrich.FromLogContext()
    .WriteTo.Console() // at least write to console
    .CreateLogger();

// Use Serilog for logging
builder.Host.UseSerilog(Log.Logger);

//==JWT Authentication & Authorization Configuration==//
var jwtSection = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSection["Key"]!);

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false; // set true in production with HTTPS
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwtSection["Issuer"],

            ValidateAudience = true,
            ValidAudience = jwtSection["Audience"],

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),

            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero // expire exactly at ExpireInMinutes
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

builder.Services.AddAuthorization(); // role-based works via ClaimTypes.Role

app.UseHsts();
app.UseHttpsRedirection();

app.UseSerilogRequestLogging();

app.UseAuthorization();

app.MapControllers();
app.Run();