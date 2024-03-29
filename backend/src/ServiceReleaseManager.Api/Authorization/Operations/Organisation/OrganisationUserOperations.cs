﻿namespace ServiceReleaseManager.Api.Authorization.Operations.Organisation;

public static class OrganisationUserOperations
{
  public static readonly OrganisationAuthorizationRequirement OrganisationUser_Create =
    new() { Name = nameof(OrganisationUser_Create), EvaluationFunction = r => r.UserWrite };

  public static readonly OrganisationAuthorizationRequirement OrganisationUser_Read =
    new() { Name = nameof(OrganisationUser_Read), EvaluationFunction = r => r.UserRead };

  public static readonly OrganisationAuthorizationRequirement OrganisationUser_List =
    new() { Name = nameof(OrganisationUser_List), EvaluationFunction = r => r.UserRead };

  public static readonly OrganisationAuthorizationRequirement OrganisationUser_Delete =
    new() { Name = nameof(OrganisationUser_Delete), EvaluationFunction = r => r.UserDelete };
}
