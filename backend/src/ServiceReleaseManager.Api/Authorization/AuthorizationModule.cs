using Autofac;

namespace ServiceReleaseManager.Api.Authorization;

public class AuthorizationModule : Module
{
  protected override void Load(ContainerBuilder builder)
  {
    builder.RegisterType<ServiceManagerAuthorizationService>()
      .As<IServiceManagerAuthorizationService>()
      .SingleInstance();

  }
}
