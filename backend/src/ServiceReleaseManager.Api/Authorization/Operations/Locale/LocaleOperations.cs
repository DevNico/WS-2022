﻿namespace ServiceReleaseManager.Api.Authorization.Operations.Locale;

public static class LocaleOperations
{
  public static readonly ServiceAuthorizationRequirement Locale_Create =
    new ServiceAuthorizationRequirement
    {
      Name = nameof(Locale_Create), EvaluationFunction = (r => r.ReleaseCreate)
    };

  public static readonly ServiceAuthorizationRequirement Locale_Read =
    new ServiceAuthorizationRequirement
    {
      Name = nameof(Locale_Read), EvaluationFunction = (_ => true)
    };

  public static readonly ServiceAuthorizationRequirement Locale_List =
    new ServiceAuthorizationRequirement
    {
      Name = nameof(Locale_List), EvaluationFunction = (_ => true)
    };

  public static readonly ServiceAuthorizationRequirement Locale_Delete =
    new ServiceAuthorizationRequirement
    {
      Name = nameof(Locale_Delete), EvaluationFunction = (r => r.ReleaseCreate)
    };
}
