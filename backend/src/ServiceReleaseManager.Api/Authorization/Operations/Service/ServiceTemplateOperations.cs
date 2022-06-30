namespace ServiceReleaseManager.Api.Authorization.Operations.Service;

public static class ServiceTemplateOperations
{
  public static readonly OrganisationAuthorizationRequirement ServiceTemplate_Create =
    new() { Name = nameof(ServiceTemplate_Create), EvaluationFunction = r => r.ServiceWrite };

  public static readonly OrganisationAuthorizationRequirement ServiceTemplate_Read =
    new() { Name = nameof(ServiceTemplate_Read), EvaluationFunction = r => true };

  public static readonly OrganisationAuthorizationRequirement ServiceTemplate_List =
    new() { Name = nameof(ServiceTemplate_List), EvaluationFunction = r => true };

  public static readonly OrganisationAuthorizationRequirement ServiceTemplate_Delete =
    new() { Name = nameof(ServiceTemplate_Delete), EvaluationFunction = r => r.ServiceDelete };
}
