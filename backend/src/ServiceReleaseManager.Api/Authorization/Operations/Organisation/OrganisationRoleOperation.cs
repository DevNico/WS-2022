namespace ServiceReleaseManager.Api.Authorization.Operations.Organisation;

public class OrganisationRoleOperation
{
  public static OrganisationAuthorizationRequirement OrganisationRole_Create = new OrganisationAuthorizationRequirement
  {
    Name = nameof(OrganisationRole_Create),
    EvaluationFunction = (r => false)
  };

  public static OrganisationAuthorizationRequirement OrganisationRole_Read = new OrganisationAuthorizationRequirement
  {
    Name = nameof(OrganisationRole_Read),
    EvaluationFunction = (r => false)
  };

  public static OrganisationAuthorizationRequirement OrganisationRole_List = new OrganisationAuthorizationRequirement
  {
    Name = nameof(OrganisationRole_List),
    EvaluationFunction = (r => false)
  };

  public static OrganisationAuthorizationRequirement OrganisationRole_Delete = new OrganisationAuthorizationRequirement
  {
    Name = nameof(OrganisationRole_Delete),
    EvaluationFunction = (r => false)
  };
}
