using AH.API.Helpers;
using AH.API.Middleware;
using AH.API.Routing;
using AH.Application.Services;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using Serilog;
using System.Reflection;
using System.Text;
using DotNetEnv;

Env.Load(); // Load .env file

var builder = WebApplication.CreateBuilder(args);

// Override appsettings with env variables
builder.Configuration["ConnectionStrings:DefaultConnection"] = Environment.GetEnvironmentVariable("DEFAULT_CONNECTION");
builder.Configuration["Jwt:Issuer"] = Environment.GetEnvironmentVariable("JWT_ISSUER");
builder.Configuration["Jwt:Audience"] = Environment.GetEnvironmentVariable("JWT_AUDIENCE");
builder.Configuration["Jwt:Key"] = Environment.GetEnvironmentVariable("JWT_KEY");
builder.Configuration["Jwt:ExpireInMinutes"] = Environment.GetEnvironmentVariable("JWT_EXPIRE_IN_MINUTES");
builder.Configuration["RefreshToken:ExpireInMinutes"] = Environment.GetEnvironmentVariable("REFRESH_TOKEN_EXPIRE_IN_MINUTES");
builder.Configuration["AllowedOrigins:0"] = Environment.GetEnvironmentVariable("ALLOWED_ORIGINS");

// -------------------- Use Autofac --------------------
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// Initialize configuration helper
ConfigHelper.Initialize(builder.Configuration);

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        // Log model binding errors
        var logger = context.HttpContext.RequestServices
            .GetRequiredService<ILogger<Program>>();

        foreach (var kvp in context.ModelState)
        {
            foreach (var error in kvp.Value.Errors)
            {
                logger.LogWarning("Model binding error on {Field}: {ErrorMessage}",
                    kvp.Key, error.ErrorMessage);
            }
        }

        // Return the default response
        return new BadRequestObjectResult(context.ModelState);
    };
});

// -------------------- Bind Options --------------------
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));
builder.Services.Configure<RefreshTokenOptions>(builder.Configuration.GetSection("RefreshToken"));
// Bind FeatureToggles so AutoRefreshTokenMiddleware can resolve IOptionsMonitor<FeatureToggles>
builder.Services.Configure<FeatureToggles>(builder.Configuration);

// Read feature flags
bool enableAuth = builder.Configuration.GetValue<bool>("EnableAuth");

// -------------------- Add Controllers --------------------
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidationFilter>();

    if (enableAuth)
    {
        var policy = new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .Build();
        options.Filters.Add(new AuthorizeFilter(policy));
    }

    options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
})
.AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ContractResolver = new DefaultContractResolver(); // PascalCase
    options.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
    options.SerializerSettings.Converters.Add(new TimeOnlyJsonConverter());
});

// -------------------- Swagger --------------------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    if (enableAuth)
    {
        // Define HTTP Bearer security scheme
        c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
        {
            Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http, // HTTP scheme
            Scheme = "bearer",                                        // "bearer"
            BearerFormat = "JWT",                                     // optional, for clarity
            In = Microsoft.OpenApi.Models.ParameterLocation.Header,
            Description = "Enter your JWT token only. Swagger will add the 'Bearer ' prefix automatically."
        });

        // Require Bearer token for all endpoints
        c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
        {
            {
                new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Reference = new Microsoft.OpenApi.Models.OpenApiReference
                    {
                        Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        });
    }

    // Optional: pretty JSON and PascalCase
    c.DescribeAllParametersInCamelCase();
});

// -------------------- JWT Authentication (conditional) --------------------
if (enableAuth)
{
    var jwtSection = builder.Configuration.GetSection("Jwt");
    var issuer = jwtSection["Issuer"] ?? string.Empty;
    var audience = jwtSection["Audience"] ?? string.Empty;

    // Ensure key length >= 32 bytes (HS256)
    var keyBytes = Encoding.UTF8.GetBytes(jwtSection["Key"] ?? string.Empty);
    if (keyBytes.Length < 32)
    {
        Array.Resize(ref keyBytes, 32); // ✅ Pad here too to match JwtService
    }

    var validateIssuer = !string.IsNullOrWhiteSpace(issuer);
    var validateAudience = !string.IsNullOrWhiteSpace(audience);

    builder.Services
        .AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = true;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = validateIssuer,
                ValidIssuer = validateIssuer ? issuer : null,
                ValidateAudience = validateAudience,
                ValidAudience = validateAudience ? audience : null,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(keyBytes), // ✅ now matches JwtService
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
        });

    builder.Services.AddAuthorization();
}

// -------------------- Autofac DI --------------------
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    var infraAssembly = Assembly.Load("AH.Infrastructure");
    var appAssembly = Assembly.Load("AH.Application");

    containerBuilder.RegisterAssemblyTypes(infraAssembly)
        .Where(t => t.Namespace?.Contains("Repositories") == true)
        .AsImplementedInterfaces()
        .InstancePerLifetimeScope();

    containerBuilder.RegisterAssemblyTypes(appAssembly)
        .Where(t => t.Namespace?.Contains("Services") == true)
        .AsImplementedInterfaces()
        .InstancePerLifetimeScope();

    containerBuilder.RegisterAssemblyTypes(appAssembly)
        .Where(t => t.Namespace?.Contains("IServices") == true)
        .AsImplementedInterfaces()
        .InstancePerLifetimeScope();

    containerBuilder.RegisterAssemblyTypes(infraAssembly)
        .Where(t => t.Namespace?.Contains("IRepositories") == true)
        .AsImplementedInterfaces()
        .InstancePerLifetimeScope();

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

// -------------------- CORS --------------------
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policyBuilder =>
    {
        policyBuilder
            .WithOrigins(builder.Configuration.GetSection("AllowedOrigins").Get<string[]>() ?? Array.Empty<string>())
            .AllowAnyMethod()
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

if (enableAuth)
{
    app.UseAutoRefreshToken();          // Refresh expired tokens
    app.UseAuthentication();           // Must come first
    app.UseAuthorization();
}

// Map controllers
app.MapControllers();

// Run
app.Run();