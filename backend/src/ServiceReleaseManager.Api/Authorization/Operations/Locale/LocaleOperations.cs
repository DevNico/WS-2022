namespace ServiceReleaseManager.Api.Authorization.Operations.Locale;

public class LocaleOperations
{
  public static ServiceAuthorizationRequirement Locale_Create = new ServiceAuthorizationRequirement
  {
    Name = nameof(Locale_Create),
    EvaluationFunction = (r => r.ReleaseCreate)
  };
  public static ServiceAuthorizationRequirement Locale_Read = new ServiceAuthorizationRequirement
  {
    Name = nameof(Locale_Read),
    EvaluationFunction = (r => true)
  };

  public static ServiceAuthorizationRequirement Locale_List = new ServiceAuthorizationRequirement
  {
    Name = nameof(Locale_List),
    EvaluationFunction = (r => true)
  };

  public static ServiceAuthorizationRequirement Locale_Delete = new ServiceAuthorizationRequirement
  {
    Name = nameof(Locale_Delete),
    EvaluationFunction = (r => r.ReleaseCreate)
  };
}
