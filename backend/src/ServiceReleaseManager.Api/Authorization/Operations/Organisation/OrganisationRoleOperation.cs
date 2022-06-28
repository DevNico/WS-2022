namespace ServiceReleaseManager.Api.Authorization.Operations.Organisation;

public class OrganisationRoleOperation
{
  public static readonly OrganisationAuthorizationRequirement OrganisationRole_Create = new OrganisationAuthorizationRequirement
  {
    Name = nameof(OrganisationRole_Create),
    EvaluationFunction = (r => false)
  };

  public static readonly OrganisationAuthorizationRequirement OrganisationRole_Read = new OrganisationAuthorizationRequirement
  {
    Name = nameof(OrganisationRole_Read),
    EvaluationFunction = (r => false)
  };

  public static readonly OrganisationAuthorizationRequirement OrganisationRole_List = new OrganisationAuthorizationRequirement
  {
    Name = nameof(OrganisationRole_List),
    EvaluationFunction = (r => false)
  };

  public static readonly OrganisationAuthorizationRequirement OrganisationRole_Delete = new OrganisationAuthorizationRequirement
  {
    Name = nameof(OrganisationRole_Delete),
    EvaluationFunction = (r => false)
  };
}
