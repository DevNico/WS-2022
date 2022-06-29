namespace ServiceReleaseManager.Api.Authorization.Operations.Service;

public static class ServiceUserOperations
{
  public static readonly OrganisationAuthorizationRequirement ServiceUser_Create =
    new OrganisationAuthorizationRequirement
    {
      Name = nameof(ServiceUser_Create), EvaluationFunction = (r => r.ServiceWrite)
    };

  public static readonly OrganisationAuthorizationRequirement ServiceUser_Read =
    new OrganisationAuthorizationRequirement
    {
      Name = nameof(ServiceUser_Read), EvaluationFunction = (r => r.ServiceWrite)
    };

  public static readonly OrganisationAuthorizationRequirement ServiceUser_List =
    new OrganisationAuthorizationRequirement
    {
      Name = nameof(ServiceUser_List), EvaluationFunction = (r => r.ServiceWrite)
    };

  public static readonly OrganisationAuthorizationRequirement ServiceUser_Delete =
    new OrganisationAuthorizationRequirement
    {
      Name = nameof(ServiceUser_Delete), EvaluationFunction = (r => r.ServiceDelete)
    };
}
