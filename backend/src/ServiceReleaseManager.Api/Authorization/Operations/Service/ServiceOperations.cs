namespace ServiceReleaseManager.Api.Authorization.Operations.Service;

public static class ServiceOperations
{
  public static readonly OrganisationAuthorizationRequirement Service_Create = new OrganisationAuthorizationRequirement
  {
    Name = nameof(Service_Create),
    EvaluationFunction = (r => r.ServiceWrite)
  };
  
  public static readonly OrganisationAuthorizationRequirement Service_Read = new OrganisationAuthorizationRequirement
  {
    Name = nameof(Service_Read),
    EvaluationFunction = (r => r.ServiceWrite)
  };

  public static readonly OrganisationAuthorizationRequirement Service_List = new OrganisationAuthorizationRequirement
  {
    Name = nameof(Service_List),
    EvaluationFunction = (r => r.ServiceWrite)
  };

  public static readonly OrganisationAuthorizationRequirement Service_Delete = new OrganisationAuthorizationRequirement
  {
    Name = nameof(Service_Delete),
    EvaluationFunction = (r => r.ServiceDelete)
  };
}
