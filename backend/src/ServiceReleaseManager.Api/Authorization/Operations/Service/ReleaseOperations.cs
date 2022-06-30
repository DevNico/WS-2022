namespace ServiceReleaseManager.Api.Authorization.Operations.Service;

public static class ReleaseOperations
{
  public static readonly ServiceAuthorizationRequirement Release_Create =
    new() { Name = nameof(Release_Create), EvaluationFunction = r => r.ReleaseCreate };

  public static readonly ServiceAuthorizationRequirement Release_Approve =
    new() { Name = nameof(Release_Approve), EvaluationFunction = r => r.ReleaseApprove };

  public static readonly ServiceAuthorizationRequirement Release_Publish =
    new() { Name = nameof(Release_Publish), EvaluationFunction = r => r.ReleasePublish };

  public static readonly ServiceAuthorizationRequirement Release_MetadataEdit =
    new() { Name = nameof(Release_MetadataEdit), EvaluationFunction = r => r.ReleaseMetadataEdit };

  public static readonly ServiceAuthorizationRequirement Release_LocalizedMetadataEdit =
    new()
    {
      Name = nameof(Release_LocalizedMetadataEdit),
      EvaluationFunction = r => r.ReleaseLocalizedMetadataEdit
    };

  public static readonly ServiceAuthorizationRequirement Release_Read =
    new() { Name = nameof(Release_Read), EvaluationFunction = _ => true };

  public static readonly ServiceAuthorizationRequirement Release_List =
    new() { Name = nameof(Release_List), EvaluationFunction = _ => true };

  public static readonly ServiceAuthorizationRequirement Release_Delete =
    new() { Name = nameof(Release_Delete), EvaluationFunction = r => r.ReleaseCreate };
}
