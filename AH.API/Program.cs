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

// -------------------- Bind Options --------------------
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));
builder.Services.Configure<RefreshTokenOptions>(builder.Configuration.GetSection("RefreshToken"));

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
});

// -------------------- Swagger --------------------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    if (enableAuth)
    {
        c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
        {
            In = Microsoft.OpenApi.Models.ParameterLocation.Header,
            Description = "Enter JWT with Bearer",
            Name = "Authorization",
            Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey
        });
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
});

// -------------------- JWT Authentication (conditional) --------------------
if (enableAuth)
{
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
            options.RequireHttpsMetadata = true;
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
    app.UseAuthentication();           // Must come first
    app.UseAutoRefreshToken();          // Refresh expired tokens
    app.UseAuthorization();
}

// Map controllers
app.MapControllers();

// Run
app.Run();