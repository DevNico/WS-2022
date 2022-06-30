using Ardalis.Result;

namespace ServiceReleaseManager.SharedKernel;

public static class ResultHelper
{
  public static Result<T> NullableSuccessNotFound<T>(T? value)
  {
    return value == null ? Result.NotFound() : Result.Success(value);
  }

  public static Result<TR> MapValue<T, TR>(this Result<T> result, Func<T, TR> map)
  {
    return result.Status switch
    {
      ResultStatus.Ok => (object)result is not Result
        ? Result.Success(map(result.Value))
        : Result.Success(),
      ResultStatus.Error => Result.Error(),
      ResultStatus.Forbidden => Result.Forbidden(),
      ResultStatus.Unauthorized => Result.Unauthorized(),
      ResultStatus.Invalid => Result.Invalid(result.ValidationErrors),
      ResultStatus.NotFound => Result.NotFound(),
      _ => throw new NotSupportedException()
    };
  }

  public static bool IsError<T>(this Result<T> result)
  {
    return result.Status == ResultStatus.Error;
  }

  public static async Task<Result<TR>> MapValue<T, TR>(
    this Task<Result<T>> resultTask,
    Func<T, TR> map
  )
  {
    var result = await resultTask;
    return result.MapValue(map);
  }
}
