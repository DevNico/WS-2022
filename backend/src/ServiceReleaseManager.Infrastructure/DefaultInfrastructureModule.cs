﻿using System.Reflection;
using Autofac;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ServiceReleaseManager.Core.Interfaces;
using ServiceReleaseManager.Core.OrganisationAggregate;
using ServiceReleaseManager.Infrastructure.Data;
using ServiceReleaseManager.Infrastructure.GitHub;
using ServiceReleaseManager.Infrastructure.GitHub.Converters;
using ServiceReleaseManager.SharedKernel;
using ServiceReleaseManager.SharedKernel.Interfaces;
using Module = Autofac.Module;

namespace ServiceReleaseManager.Infrastructure;

public class DefaultInfrastructureModule : Module
{
  private readonly List<Assembly> _assemblies = new();
  private readonly IConfiguration _configuration;
  private readonly string _contentRootPath;
  private readonly bool _isDevelopment;

  public DefaultInfrastructureModule(
    bool isDevelopment,
    IConfiguration configuration,
    string contentRootPath,
    Assembly? callingAssembly = null
  )
  {
    _isDevelopment = isDevelopment;
    _configuration = configuration;
    _contentRootPath = contentRootPath;
    var coreAssembly = Assembly.GetAssembly(typeof(Organisation));
    var infrastructureAssembly = Assembly.GetAssembly(typeof(StartupSetup));
    if (coreAssembly != null)
    {
      _assemblies.Add(coreAssembly);
    }

    if (infrastructureAssembly != null)
    {
      _assemblies.Add(infrastructureAssembly);
    }

    if (callingAssembly != null)
    {
      _assemblies.Add(callingAssembly);
    }
  }

  protected override void Load(ContainerBuilder builder)
  {
    if (_isDevelopment)
    {
      RegisterDevelopmentOnlyDependencies(builder);
    }
    else
    {
      RegisterProductionOnlyDependencies(builder);
    }

    RegisterCommonDependencies(builder);
  }

  private void RegisterCommonDependencies(ContainerBuilder builder)
  {
    builder.RegisterGeneric(typeof(EfRepository<>))
           .As(typeof(IRepository<>))
           .As(typeof(IReadRepository<>))
           .InstancePerLifetimeScope();

    builder
     .RegisterType<Mediator>()
     .As<IMediator>()
     .InstancePerLifetimeScope();

    builder
     .RegisterType<DomainEventDispatcher>()
     .As<IDomainEventDispatcher>()
     .InstancePerLifetimeScope();

    builder.Register<ServiceFactory>(
      context =>
      {
        var c = context.Resolve<IComponentContext>();
        return t => c.Resolve(t);
      }
    );

    var mediatrOpenTypes = new[]
    {
      typeof(IRequestHandler<,>), typeof(IRequestExceptionHandler<,,>),
      typeof(IRequestExceptionAction<,>), typeof(INotificationHandler<>)
    };

    foreach (var mediatrOpenType in mediatrOpenTypes)
    {
      builder
       .RegisterAssemblyTypes(_assemblies.ToArray())
       .AsClosedTypesOf(mediatrOpenType)
       .AsImplementedInterfaces();
    }

    builder.RegisterType<EmailSender>().As<IEmailSender>()
           .InstancePerLifetimeScope();

    builder.Register(
              c =>
                new KeycloakOAuthClient(
                  _configuration,
                  c.Resolve<ILogger<KeycloakOAuthClient>>(),
                  new HttpClient()
                )
            )
           .InstancePerLifetimeScope();

    builder.Register(
              c => new KeycloakClient(
                _configuration,
                c.Resolve<ILogger<KeycloakClient>>(),
                c.Resolve<KeycloakOAuthClient>(),
                new HttpClient()
              )
            )
           .As<IKeycloakClient>()
           .InstancePerLifetimeScope();

    builder.Register(c => new GitHubProxy(_configuration))
           .As<IGitHubProxy>()
           .InstancePerLifetimeScope();

    builder.Register(c => new ChangeLogConverter())
           .As<IChangeLogConverter>()
           .InstancePerLifetimeScope();

    builder.Register(
              c =>
                new MetadataFormatValidator(
                  c.Resolve<ILogger<MetadataFormatValidator>>(),
                  _contentRootPath
                )
            )
           .As<IMetadataFormatValidator>()
           .InstancePerLifetimeScope();
  }

  private void RegisterDevelopmentOnlyDependencies(ContainerBuilder builder)
  {
    // TODO: Add development only services
  }

  private void RegisterProductionOnlyDependencies(ContainerBuilder builder)
  {
    // TODO: Add production only services
  }
}
