using Autofac;
using ServiceReleaseManager.Core.Interfaces;
using ServiceReleaseManager.Core.Services;

namespace ServiceReleaseManager.Core;

public class DefaultCoreModule : Module
{
  protected override void Load(ContainerBuilder builder)
  {
    builder.RegisterType<LocaleService>()
      .As<ILocaleService>()
      .InstancePerLifetimeScope();

    builder.RegisterType<OrganisationRoleService>()
      .As<IOrganisationRoleService>()
      .InstancePerLifetimeScope();
    
    builder.RegisterType<OrganisationService>()
      .As<IOrganisationService>()
      .InstancePerLifetimeScope();
    
    builder.RegisterType<OrganisationUserService>()
      .As<IOrganisationUserService>()
      .InstancePerLifetimeScope();
    
    builder.RegisterType<ServiceService>()
      .As<IServiceService>()
      .InstancePerLifetimeScope();
  }
}
