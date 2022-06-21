using System.Reflection;
using Autofac;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.Configuration;
using ServiceReleaseManager.Core.Interfaces;
using ServiceReleaseManager.Core.OrganisationAggregate;
using ServiceReleaseManager.Infrastructure.Data;
using ServiceReleaseManager.SharedKernel;
using ServiceReleaseManager.SharedKernel.Interfaces;
using Module = Autofac.Module;

namespace ServiceReleaseManager.Infrastructure;

public class DefaultInfrastructureModule : Module
{
  private readonly List<Assembly> _assemblies = new();
  private readonly IConfiguration _configuration;
  private readonly bool _isDevelopment;

  public DefaultInfrastructureModule(bool isDevelopment, IConfiguration configuration,
    Assembly? callingAssembly = null)
  {
    _isDevelopment = isDevelopment;
    _configuration = configuration;
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

    builder.Register<ServiceFactory>(context =>
    {
      var c = context.Resolve<IComponentContext>();
      return t => c.Resolve(t);
    });

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

    builder.Register(_ => new KeycloakClient(_configuration)).As<IKeycloakClient>()
      .InstancePerLifetimeScope();

    builder.RegisterType<MetadataFormatValidator>()
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
