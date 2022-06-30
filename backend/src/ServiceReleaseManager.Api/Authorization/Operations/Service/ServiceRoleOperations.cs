namespace ServiceReleaseManager.Api.Authorization.Operations.Service;

public static class ServiceRoleOperations
{
  public static readonly OrganisationAuthorizationRequirement ServiceRole_Create =
    new() { Name = nameof(ServiceRole_Create), EvaluationFunction = r => r.ServiceWrite };

  public static readonly OrganisationAuthorizationRequirement ServiceRole_Read =
    new() { Name = nameof(ServiceRole_Read), EvaluationFunction = r => r.ServiceWrite };

  public static readonly OrganisationAuthorizationRequirement ServiceRole_List =
    new() { Name = nameof(ServiceRole_List), EvaluationFunction = r => r.ServiceWrite };

  public static readonly OrganisationAuthorizationRequirement ServiceRole_Delete =
    new() { Name = nameof(ServiceRole_Delete), EvaluationFunction = r => r.ServiceDelete };
}
