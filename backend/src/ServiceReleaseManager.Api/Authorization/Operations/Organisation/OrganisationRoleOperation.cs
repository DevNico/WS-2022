namespace ServiceReleaseManager.Api.Authorization.Operations.Organisation;

public static class OrganisationRoleOperation
{
  public static readonly OrganisationAuthorizationRequirement OrganisationRole_Create = new OrganisationAuthorizationRequirement
  {
    Name = nameof(OrganisationRole_Create),
    EvaluationFunction = (_ => false)
  };

  public static readonly OrganisationAuthorizationRequirement OrganisationRole_Read = new OrganisationAuthorizationRequirement
  {
    Name = nameof(OrganisationRole_Read),
    EvaluationFunction = (_ => false)
  };

  public static readonly OrganisationAuthorizationRequirement OrganisationRole_List = new OrganisationAuthorizationRequirement
  {
    Name = nameof(OrganisationRole_List),
    EvaluationFunction = (_ => false)
  };

  public static readonly OrganisationAuthorizationRequirement OrganisationRole_Delete = new OrganisationAuthorizationRequirement
  {
    Name = nameof(OrganisationRole_Delete),
    EvaluationFunction = (_ => false)
  };
}
