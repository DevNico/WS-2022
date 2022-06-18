using System.Net;
using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Core.Interfaces;
using ServiceReleaseManager.Core.OrganisationAggregate;
using ServiceReleaseManager.SharedKernel;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.OrganisationUsers;

public class Create : EndpointBase.WithRequest<CreateOrganisationUserRequest>.WithActionResult<
  OrganisationUserRecord>
{
  private readonly IOrganisationUserService _organisationUserService;
  private readonly IOrganisationService _organisationService;
  private readonly IKeycloakClient _keycloakClient;
  private readonly ILogger<Create> _logger;

  public Create(IOrganisationUserService organisationUserService, IKeycloakClient keycloakClient,
    ILogger<Create> logger, IOrganisationService organisationService)
  {
    _organisationUserService = organisationUserService;
    _keycloakClient = keycloakClient;
    _logger = logger;
    _organisationService = organisationService;
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
  [SwaggerResponse(404, "Organisation or email not found")]
  [SwaggerResponse(424, "The keycloak request failed")]
  public override async Task<ActionResult<OrganisationUserRecord>> HandleAsync(
    [FromBody] CreateOrganisationUserRequest request,
    CancellationToken cancellationToken = new())
  {
    var organisationResult =
      await _organisationService.GetById(request.OrganisationId, cancellationToken);
    if (!organisationResult.IsSuccess)
    {
      return NotFound();
    }

    KeycloakUserRecord keycloakUser;
    try
    {
      keycloakUser = await _keycloakClient.GetUserByEmail(request.Email);
    }
    catch (HttpRequestException e)
    {
      if (e.StatusCode == HttpStatusCode.NotFound)
      {
        return NotFound();
      }

      _logger.LogError("The request to the keycloak client failed with code {} and message {}",
        e.StatusCode.ToString(), e.Message);
      return StatusCode(StatusCodes.Status424FailedDependency, "Keycloak client request failed");
    }

    var newUser = new OrganisationUser(
      keycloakUser.Id,
      request.Email,
      request.FirstName,
      request.LastName,
      request.RoleId
    );
    var result = await _organisationUserService.Create(newUser, cancellationToken);

    var organisation = organisationResult.Value;
    organisation.Users.Add(result);
    await _organisationService.Update(organisation, cancellationToken);

    return this.ToActionResult(result.MapValue(OrganisationUserRecord.FromEntity));
  }
}
