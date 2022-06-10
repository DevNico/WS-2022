using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Api.Routes;
using ServiceReleaseManager.Core.OrganisationAggregate;
using ServiceReleaseManager.Core.OrganisationAggregate.Specifications;
using ServiceReleaseManager.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.OrganisationUserEndpoints;

public class Create : EndpointBaseAsync
  .WithRequest<CreateOrganisationUserRequest>
  .WithActionResult<OrganisationUserRecord>
{
  private readonly IRepository<Organisation> _repository;

  public Create(IRepository<Organisation> repository)
  {
    _repository = repository;
  }

  [HttpPost(RouteHelper.OrganizationUsers_Create)]
  [Authorize(Roles = "superAdmin")]
  [SwaggerOperation(
    Summary = "Creates a new OrganisationUser",
    Description = "Creates a new OrganisationUser",
    OperationId = "OrganisationUser.Create",
    Tags = new[] { "OrganisationUserEndpoints" })
  ]
  public override async Task<ActionResult<OrganisationUserRecord>> HandleAsync(CreateOrganisationUserRequest request, CancellationToken cancellationToken = new())
  {

    if (string.IsNullOrWhiteSpace(request.UserId)|| string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.OrganisationName))
    {
      return BadRequest();
    }

    var orgSpec = new OrganisationByNameSpec(request.OrganisationName);
    var org = await _repository.GetBySpecAsync(orgSpec, cancellationToken);
    if (org == null)
    {
      return Unauthorized();
    }

    var spec = new OrganisationUserByUserIdSpec(request.UserId);
    var found = spec.Evaluate(org.Users).FirstOrDefault();
    if (found != null)
    {
      return Conflict();
    }

    var newUser = new OrganisationUser(request.UserId, request.Email, false, request.FirstName ?? "", request.LastName ?? "", null);  
    org.Users.Add(newUser);
    await _repository.UpdateAsync(org);
    await _repository.SaveChangesAsync();

    var response = OrganisationUserRecord.FromEntity(newUser);
    return Ok(response);
  }
}
