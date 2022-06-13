using Ardalis.ListStartupServices;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using ServiceReleaseManager.Api;
using ServiceReleaseManager.Api.Authorization;
using ServiceReleaseManager.Api.OpenApi;
using ServiceReleaseManager.Core;
using ServiceReleaseManager.Infrastructure;
using ServiceReleaseManager.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.UseSerilog((_, config) => config.ReadFrom.Configuration(builder.Configuration));

var config = builder.Configuration
  .AddJsonFile("appsettings.json")
  .AddJsonFile("appsettings.Production.json", true)
  .AddEnvironmentVariables()
  .Build();

var swaggerConfig = config.GetSection("Swagger");
var kcConfig = config.GetSection("Keycloak");
var kcBaseUrl = $"{kcConfig["Url"]}/realms/{kcConfig["Realm"]}";

var connectionString = config.GetConnectionString("Default");
builder.Services.AddDbContext(connectionString);

builder.Services.AddControllers();
builder.Services.AddHealthChecks();

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
  options.ForwardedHeaders = ForwardedHeaders.All;
});

// Cors
builder.Services.AddCors(options => options.AddDefaultPolicy(b =>
{
  // TODO: Add CORS policy
  b.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
}));

// Authentication
if(builder.Environment.EnvironmentName != "Testing")
{
  builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(o =>
    {
      o.Authority = kcBaseUrl;
      o.Audience = kcConfig["Audience"];
      o.TokenValidationParameters.NameClaimType = "preferred_username";
      o.TokenValidationParameters.RoleClaimType = "role";
    });
  builder.Services.AddTransient<IClaimsTransformation>(_ =>
    new KeycloakRolesClaimsTransformation("role"));
}


// Authorization
builder.Services.AddAuthorization(options =>
{
  options.FallbackPolicy = new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser()
    .Build();
});


// Swagger
builder.Services.AddSwaggerGen(c =>
{
  c.SwaggerDoc("v1", new OpenApiInfo { Title = "Service Release Manager API", Version = "v1" });
  c.EnableAnnotations();
  var securityDefinition = new OpenApiSecurityScheme
  {
    Type = SecuritySchemeType.OAuth2,
    Flows = new OpenApiOAuthFlows
    {
      AuthorizationCode = new OpenApiOAuthFlow
      {
        AuthorizationUrl = new Uri($"{kcBaseUrl}/protocol/openid-connect/auth"),
        TokenUrl = new Uri($"{kcBaseUrl}/protocol/openid-connect/token")
      }
    }
  };
  c.AddSecurityDefinition("OAuth2", securityDefinition);
  c.OperationFilter<AuthorizeOperationFilter>();
});

// add list services for diagnostic purposes - see https://github.com/ardalis/AspNetCoreStartupServices
builder.Services.Configure<ServiceConfig>(serviceConfig =>
{
  serviceConfig.Services = new List<ServiceDescriptor>(builder.Services);

  // optional - default path to view services is /listallservices - recommended to choose your own path
  serviceConfig.Path = "/listservices";
});


builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
  containerBuilder.RegisterModule(new DefaultCoreModule());
  containerBuilder.RegisterModule(
    new DefaultInfrastructureModule(builder.Environment.EnvironmentName == "Development", config));
});

var app = builder.Build();

app.UseForwardedHeaders();

if (app.Environment.IsDevelopment())
{
  // Show services diagnostic endpoint
  app.UseShowAllServicesMiddleware();
  app.UseDeveloperExceptionPage();
}

if (swaggerConfig.GetValue<bool>("Enabled"))
{
  // Enable middleware to serve generated Swagger as a JSON endpoint.
  app.UseSwagger();

  // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
  app.UseSwaggerUI(c =>
  {
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Service Release Manager API V1");
    c.OAuthClientId(swaggerConfig["ClientId"]);
    c.OAuthAppName("SRM API");
    c.OAuthScopeSeparator(" ");
    c.OAuthUsePkce();
  });
}

app.UseRouting();
app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapHealthChecks("/healthz")
  .AllowAnonymous();

app.UseEndpoints(endpoints =>
{
  endpoints.MapDefaultControllerRoute();
});

// Seed Database
using (var scope = app.Services.CreateScope())
{
  var services = scope.ServiceProvider;

  try
  {
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.Migrate();
    // context.Database.EnsureCreated();
    SeedData.Initialize(services);
  }
  catch (Exception ex)
  {
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred seeding the DB. {ExceptionMessage}", ex.Message);
  }
}

app.Run();
