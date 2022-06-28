namespace ServiceReleaseManager.Api.Authorization.Operations.Service;

public class ServiceTemplateOperations
{

  public static OrganisationAuthorizationRequirement ServiceTemplate_Create = new OrganisationAuthorizationRequirement
  {
    Name = nameof(ServiceTemplate_Create),
    EvaluationFunction = (r => r.ServiceWrite)
  };
  public static OrganisationAuthorizationRequirement ServiceTemplate_Read = new OrganisationAuthorizationRequirement
  {
    Name = nameof(ServiceTemplate_Read),
    EvaluationFunction = (r => true)
  };

  public static OrganisationAuthorizationRequirement ServiceTemplate_List = new OrganisationAuthorizationRequirement
  {
    Name = nameof(ServiceTemplate_List),
    EvaluationFunction = (r => true)
  };

  public static OrganisationAuthorizationRequirement ServiceTemplate_Delete = new OrganisationAuthorizationRequirement
  {
    Name = nameof(ServiceTemplate_Delete),
    EvaluationFunction = (r => r.ServiceDelete)
  };
}
