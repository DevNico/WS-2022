using System.Net;
using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Core.Interfaces;
using ServiceReleaseManager.Core.OrganisationAggregate;
using ServiceReleaseManager.SharedKernel;
using ServiceReleaseManager.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.OrganisationUsers;

public class Create : EndpointBase.WithRequest<CreateOrganisationUserRequest>.WithActionResult<
  OrganisationUserRecord>
{
  private readonly IKeycloakClient _keycloakClient;
  private readonly ILogger<Create> _logger;
  private readonly IReadRepository<OrganisationRole> _organisationRoleRepository;
  private readonly IOrganisationService _organisationService;
  private readonly IOrganisationUserService _organisationUserService;

  public Create(
    IOrganisationUserService organisationUserService,
    IKeycloakClient keycloakClient,
    ILogger<Create> logger,
    IOrganisationService organisationService,
    IReadRepository<OrganisationRole> organisationRoleRepository)
  {
    _organisationUserService = organisationUserService;
    _keycloakClient = keycloakClient;
    _logger = logger;
    _organisationService = organisationService;
    _organisationRoleRepository = organisationRoleRepository;
  }

  private async Task<KeycloakUserFetchResult> GetKeycloakUser(string email)
  {
    try
    {
      var result = await _keycloakClient.GetUserByEmail(email);
      if (result != null)
      {
        return new KeycloakUserFetchResult(result, false);
      }
    }
    catch (HttpRequestException e)
    {
      if (e.StatusCode != HttpStatusCode.NotFound)
      {
        _logger.LogError(
          "The request to the keycloak client failed with code {Status} and message {Message}",
          e.StatusCode.ToString(), e.Message);
        if (e.InnerException != null)
        {
          _logger.LogError("Inner exception message: {Message}", e.InnerException.Message);
        }

        return new KeycloakUserFetchResult(null, true);
      }
    }

    return new KeycloakUserFetchResult(null, false);
  }

  [HttpPost]
  [SwaggerOperation(
    Summary = "Creates a new OrganisationUser",
    Description = "Creates a new OrganisationUser",
    OperationId = "OrganisationUser.Create",
    Tags = new[] { "OrganisationUser" })
  ]
  [SwaggerResponse(200, "User created", typeof(OrganisationUserRecord))]
  [SwaggerResponse(400, "Invalid request")]
  [SwaggerResponse(400, "Organisation or email not found")]
  [SwaggerResponse(424, "The keycloak request failed")]
  public override async Task<ActionResult<OrganisationUserRecord>> HandleAsync(
    [FromBody] CreateOrganisationUserRequest request,
    CancellationToken cancellationToken = new())
  {
    var organisationResult =
      await _organisationService.GetById(request.OrganisationId, cancellationToken);
    if (!organisationResult.IsSuccess)
    {
      return BadRequest();
    }

    var roleResult =
      await _organisationRoleRepository.GetByIdAsync(request.RoleId, cancellationToken);

    if (roleResult == null)
    {
      return BadRequest();
    }


    var keycloakUser = await GetKeycloakUser(request.Email);
    if (keycloakUser.RequestFailed)
    {
      return StatusCode(StatusCodes.Status424FailedDependency, "Keycloak client request failed");
    }

    if (keycloakUser.User == null)
    {
      try
      {
        await _keycloakClient.CreateUser(new KeycloakUserCreation(request.Email,
          request.FirstName, request.LastName, request.Email));
      }
      catch (HttpRequestException e)
      {
        _logger.LogError("Keycloak user creation failed with code {Status} and message {Message}",
          e.StatusCode.ToString(), e.Message);

        return StatusCode(StatusCodes.Status424FailedDependency,
          "Keycloak client create request failed");
      }
    }

    keycloakUser = await GetKeycloakUser(request.Email);
    if (keycloakUser.RequestFailed)
    {
      return StatusCode(StatusCodes.Status424FailedDependency, "Keycloak client request failed");
    }

    if (keycloakUser.User == null)
    {
      _logger.LogError("Keycloak did not return a user record");
      return StatusCode(StatusCodes.Status424FailedDependency,
        "Keycloak client create request failed");
    }

    var newUser = new OrganisationUser(
      keycloakUser.User.Id,
      request.Email.ToLower(),
      request.FirstName,
      request.LastName,
      roleResult,
      request.OrganisationId
    );

    var result = await _organisationUserService.Create(newUser, cancellationToken);

    return this.ToActionResult(result.MapValue(OrganisationUserRecord.FromEntity));
  }
}

internal record KeycloakUserFetchResult(
  KeycloakUserRecord? User,
  bool RequestFailed
);
