namespace ServiceReleaseManager.Api.Authorization.Operations.Service;

public class ServiceRoleOperations
{
  public static OrganisationAuthorizationRequirement ServiceRole_Create = new OrganisationAuthorizationRequirement
  {
    Name = nameof(ServiceRole_Create),
    EvaluationFunction = (r => r.ServiceWrite)
  };
  public static OrganisationAuthorizationRequirement ServiceRole_Read = new OrganisationAuthorizationRequirement
  {
    Name = nameof(ServiceRole_Read),
    EvaluationFunction = (r => r.ServiceWrite)
  };

  public static OrganisationAuthorizationRequirement ServiceRole_List = new OrganisationAuthorizationRequirement
  {
    Name = nameof(ServiceRole_List),
    EvaluationFunction = (r => r.ServiceWrite)
  };

  public static OrganisationAuthorizationRequirement ServiceRole_Delete = new OrganisationAuthorizationRequirement
  {
    Name = nameof(ServiceRole_Delete),
    EvaluationFunction = (r => r.ServiceDelete)
  };
}
