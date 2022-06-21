using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ServiceReleaseManager.Api.Endpoints;

public static class EndpointBase
{
  public const string VersionBase = "/api/v{version:apiVersion}";
  public const string NamspaceBase = $"{VersionBase}/[namespace]";

  public static class WithRequest<TRequest>
  {
    [Authorize]
    [Route(NamspaceBase)]
    public abstract class
      WithActionResult<TResponse> : EndpointBaseAsync.WithRequest<TRequest>.WithActionResult<
        TResponse>
    {
    }

    [Authorize]
    [Route(NamspaceBase)]
    public abstract class WithoutResult : EndpointBaseAsync.WithRequest<TRequest>.WithoutResult
    {
    }
  }

  public static class WithoutRequest
  {
    [Authorize]
    [Route(NamspaceBase)]
    public abstract class
      WithActionResult<TResponse> : EndpointBaseAsync.WithoutRequest.WithActionResult<TResponse>
    {
    }

    [Authorize]
    [Route(NamspaceBase)]
    public abstract class WithoutResult : EndpointBaseAsync.WithoutRequest.WithoutResult
    {
    }
  }
}
