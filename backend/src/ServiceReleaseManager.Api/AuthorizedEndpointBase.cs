using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Api.Routes;

namespace ServiceReleaseManager.Api;

public static class AuthorizedEndpointBase
{
  public static class WithRequest<TRequest>
  {
    [Authorize]
    [Route(RouteHelper.BaseRoute)]
    public abstract class
      WithActionResult<TResponse> : EndpointBaseAsync.WithRequest<TRequest>.WithActionResult<TResponse>
    {
    }

    [Authorize]
    [Route(RouteHelper.BaseRoute)]
    public abstract class WithoutResult : EndpointBaseAsync.WithRequest<TRequest>.WithoutResult
    {
    }
  }

  public static class WithoutRequest
  {
    [Authorize]
    [Route(RouteHelper.BaseRoute)]
    public abstract class WithActionResult<TResponse> : EndpointBaseAsync.WithoutRequest.WithActionResult<TResponse>
    {
    }

    [Authorize]
    [Route(RouteHelper.BaseRoute)]
    public abstract class WithoutResult : EndpointBaseAsync.WithoutRequest.WithoutResult
    {
    }
  }
}
