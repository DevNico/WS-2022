namespace ServiceReleaseManager.Api.Authorization.Operations.Service;

public static class ReleaseOperations
{
  public static readonly ServiceAuthorizationRequirement Release_Create =
    new ServiceAuthorizationRequirement
    {
      Name = nameof(Release_Create), EvaluationFunction = (r => r.ReleaseCreate)
    };

  public static readonly ServiceAuthorizationRequirement Release_Approve =
    new ServiceAuthorizationRequirement
    {
      Name = nameof(Release_Approve), EvaluationFunction = (r => r.ReleaseApprove)
    };

  public static readonly ServiceAuthorizationRequirement Release_Publish =
    new ServiceAuthorizationRequirement
    {
      Name = nameof(Release_Publish), EvaluationFunction = (r => r.ReleasePublish)
    };

  public static readonly ServiceAuthorizationRequirement Release_MetadataEdit =
    new ServiceAuthorizationRequirement
    {
      Name = nameof(Release_MetadataEdit), EvaluationFunction = (r => r.ReleaseMetadataEdit)
    };

  public static readonly ServiceAuthorizationRequirement Release_LocalizedMetadataEdit =
    new ServiceAuthorizationRequirement
    {
      Name = nameof(Release_LocalizedMetadataEdit),
      EvaluationFunction = (r => r.ReleaseLocalizedMetadataEdit)
    };

  public static readonly ServiceAuthorizationRequirement Release_Read =
    new ServiceAuthorizationRequirement
    {
      Name = nameof(Release_Read), EvaluationFunction = (_ => true)
    };

  public static readonly ServiceAuthorizationRequirement Release_List =
    new ServiceAuthorizationRequirement
    {
      Name = nameof(Release_List), EvaluationFunction = (_ => true)
    };

  public static readonly ServiceAuthorizationRequirement Release_Delete =
    new ServiceAuthorizationRequirement
    {
      Name = nameof(Release_Delete), EvaluationFunction = (r => r.ReleaseCreate)
    };
}
