using Ardalis.ListStartupServices;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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

var connectionString = builder.Configuration.GetConnectionString("Default");

builder.Services.AddDbContext(connectionString);
builder.Services.AddControllers();

// Cors
builder.Services.AddCors(options => options.AddDefaultPolicy(b =>
{
  // TODO: Add CORS policy
  b.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
}));


// Authentication
builder.Services
  .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
  .AddJwtBearer(o =>
  {
    o.Authority = builder.Configuration["Jwt:Authority"];
    o.Audience = builder.Configuration["Jwt:Audience"];
    o.TokenValidationParameters.NameClaimType = "preferred_username";
    o.TokenValidationParameters.RoleClaimType = "role";
  });
builder.Services.AddTransient<IClaimsTransformation>(_ =>
  new KeycloakRolesClaimsTransformation("role"));


// Authorization
builder.Services.AddAuthorization(options =>
{
  options.FallbackPolicy = new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser()
    .Build();
});
builder.Services.AddSingleton<IAuthorizationHandler, OrganisationAuthorizationHandler>();


// Swagger
builder.Services.AddSwaggerGen(c =>
{
  c.SwaggerDoc("v1", new OpenApiInfo { Title = "Service Release Manager API", Version = "v1" });
  c.EnableAnnotations();
  c.DescribeAllParametersInCamelCase();
  var securityDefinition = new OpenApiSecurityScheme
  {
    Type = SecuritySchemeType.OAuth2,
    Flows = new OpenApiOAuthFlows
    {
      AuthorizationCode = new OpenApiOAuthFlow
      {
        // TODO: Move to configuration
        AuthorizationUrl =
          new Uri(
            "https://idp.srm.k3s.devnico.cloud/realms/dev/protocol/openid-connect/auth"),
        TokenUrl =
          new Uri(
            "https://idp.srm.k3s.devnico.cloud/realms/dev/protocol/openid-connect/token")
      }
    }
  };
  c.AddSecurityDefinition("OAuth2", securityDefinition);
  c.OperationFilter<AuthorizeOperationFilter>();
});


// add list services for diagnostic purposes - see https://github.com/ardalis/AspNetCoreStartupServices
builder.Services.Configure<ServiceConfig>(config =>
{
  config.Services = new List<ServiceDescriptor>(builder.Services);

  // optional - default path to view services is /listallservices - recommended to choose your own path
  config.Path = "/listservices";
});


builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
  containerBuilder.RegisterModule(new DefaultCoreModule());
  containerBuilder.RegisterModule(new DefaultInfrastructureModule(
    builder.Environment.EnvironmentName == "Development"));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  // Show services diagnostic endpoint
  app.UseShowAllServicesMiddleware();
  app.UseDeveloperExceptionPage();

  // Enable middleware to serve generated Swagger as a JSON endpoint.
  app.UseSwagger();

  // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
  app.UseSwaggerUI(c =>
  {
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Service Release Manager API V1");
    c.OAuthClientId("webapp-v1");
    c.OAuthAppName("SRM API");
    c.OAuthScopeSeparator(" ");
    c.OAuthUsePkce();
  });
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

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
    context.Database.EnsureCreated();
    SeedData.Initialize(services);
  }
  catch (Exception ex)
  {
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred seeding the DB. {ExceptionMessage}", ex.Message);
  }
}

app.Run();
