using AH.API.Middleware;
using AH.API.Routing;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using Serilog;
using System.Reflection;
using System.Text;
using AH.Application.Services;

var builder = WebApplication.CreateBuilder(args);

// -------------------- Use Autofac --------------------
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// Initialize configuration helper
ConfigHelper.Initialize(builder.Configuration);

// -------------------- Add Controllers --------------------
builder.Services.AddControllers(options =>
{
    // Global validation filter
    options.Filters.Add<ValidationFilter>();

    // Global authorize filter (all endpoints require authentication by default)
    var policy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
    options.Filters.Add(new AuthorizeFilter(policy));

    // Slugify convention for [controller] routes
    options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
})
.AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ContractResolver = new DefaultContractResolver(); // PascalCase
    options.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
});

// -------------------- Swagger --------------------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// -------------------- Autofac DI --------------------
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    // Repositories
    containerBuilder.RegisterAssemblyTypes(Assembly.Load("AH.Infrastructure"))
        .Where(t => t.Namespace == "AH.Infrastructure.Repositories")
        .AsImplementedInterfaces()
        .InstancePerLifetimeScope();

    // Services
    containerBuilder.RegisterAssemblyTypes(Assembly.Load("AH.Application"))
        .Where(t => t.Namespace == "AH.Application.Services")
        .AsImplementedInterfaces()
        .InstancePerLifetimeScope();

    // IServices
    containerBuilder.RegisterAssemblyTypes(Assembly.Load("AH.Application"))
        .Where(t => t.Namespace == "AH.Application.IServices")
        .AsImplementedInterfaces()
        .InstancePerLifetimeScope();

    // IRepositories
    containerBuilder.RegisterAssemblyTypes(Assembly.Load("AH.Infrastructure"))
        .Where(t => t.Namespace == "AH.Infrastructure.IRepositories")
        .AsImplementedInterfaces()
        .InstancePerLifetimeScope();

    // Configuration
    containerBuilder.Register(ctx => builder.Configuration)
        .As<IConfiguration>()
        .SingleInstance();
});

// -------------------- Serilog --------------------
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

builder.Host.UseSerilog(Log.Logger);

// -------------------- JWT Authentication --------------------
var jwtSection = builder.Configuration.GetSection("Jwt");
var refreshTokenSection = builder.Configuration.GetSection("RefreshToken");
var key = Encoding.UTF8.GetBytes(jwtSection["Key"]!);

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false; // true in production
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
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization(); // Enables role-based auth via ClaimTypes.Role

builder.Services.Configure<JwtOptions>(jwtSection);
builder.Services.Configure<RefreshTokenOptions>(refreshTokenSection);

//--------------------- CORS -------------------

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policyBuilder =>
    {
        policyBuilder.WithOrigins
        (builder.Configuration.GetSection("AllowedOrigins")
        .Get<string[]>() ?? []).AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// -------------------- Build App --------------------
var app = builder.Build();

// -------------------- Middleware Pipeline --------------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHsts();
app.UseHttpsRedirection();
app.UseSerilogRequestLogging();

app.UseCors();
// Authentication first
app.UseAuthentication();

// Auto-refresh middleware after authentication
app.UseAutoRefreshToken();

// Authorization after refresh middleware
app.UseAuthorization();

// Map controllers
app.MapControllers();

// Run the app
app.Run();