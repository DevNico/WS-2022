namespace ServiceReleaseManager.Api.Authorization.Operations.Organisation;

public class OrganisationUserOperations
{
  public static OrganisationAuthorizationRequirement OrganisationUser_Create = new OrganisationAuthorizationRequirement
  {
    Name = nameof(OrganisationUser_Create),
    EvaluationFunction = (r => r.UserWrite)
  };

  public static OrganisationAuthorizationRequirement OrganisationUser_Read = new OrganisationAuthorizationRequirement
  {
    Name = nameof(OrganisationUser_Read),
    EvaluationFunction = (r => r.UserRead)
  };

  public static OrganisationAuthorizationRequirement OrganisationUser_List = new OrganisationAuthorizationRequirement
  {
    Name = nameof(OrganisationUser_List),
    EvaluationFunction = (r => r.UserRead)
  };

  public static OrganisationAuthorizationRequirement OrganisationUser_Delete = new OrganisationAuthorizationRequirement
  {
    Name = nameof(OrganisationUser_Delete),
    EvaluationFunction = (r => r.UserDelete)
  };

}
