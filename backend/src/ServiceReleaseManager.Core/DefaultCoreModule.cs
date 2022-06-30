using Autofac;
using ServiceReleaseManager.Core.GitHub;
using ServiceReleaseManager.Core.GitHub.Converters;
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

    builder.RegisterType<ReleaseService>()
      .As<IReleaseService>()
      .InstancePerLifetimeScope();
    
    builder.RegisterType<ReleaseTriggerService>()
      .As<IReleaseTriggerService>()
      .InstancePerLifetimeScope();

    builder.RegisterType<ServiceUserService>()
      .As<IServiceUserService>()
      .InstancePerLifetimeScope();
    
    builder.RegisterType<ServiceRoleService>()
      .As<IServiceRoleService>()
      .InstancePerLifetimeScope();

    builder.RegisterType<ChangeLogConverter>()
      .As<IChangeLogConverter>()
      .InstancePerLifetimeScope();

    builder.RegisterType<GitHubProxy>()
      .As<IGitHubProxy>()
      .InstancePerLifetimeScope();
  }
}
