using Ardalis.Result;
using Newtonsoft.Json;
using ServiceReleaseManager.Core.Interfaces;
using ServiceReleaseManager.Core.OrganisationAggregate;
using ServiceReleaseManager.Core.OrganisationAggregate.Specifications;
using ServiceReleaseManager.SharedKernel.Interfaces;

namespace ServiceReleaseManager.Core.Services;

public class OrganisationRoleService : IOrganisationRoleService
{
  private readonly IRepository<Organisation> _organisationRepository;
  private readonly IRepository<OrganisationRole> _roleRepository;

  public OrganisationRoleService(IRepository<Organisation> organisationRepository,
    IRepository<OrganisationRole> roleRepository)
  {
    _organisationRepository = organisationRepository;
    _roleRepository = roleRepository;
  }

  public async Task<Result<List<OrganisationRole>>> ListByOrganisationRouteName(
    string organisationRouteName,
    CancellationToken cancellationToken)
  {
    var spec = new OrganisationRolesByOrganisationRouteNameSpec(organisationRouteName);
    var roles =
      await _organisationRepository.GetBySpecAsync<IEnumerable<OrganisationRole>>(spec,
        cancellationToken);

    return Result.Success((roles ?? new List<OrganisationRole>()).ToList());
  }

  public async Task<Result<OrganisationRole>> Create(OrganisationRole role,
    CancellationToken cancellationToken)
  {
    var organisationSpec = new OrganisationByIdSpec(role.OrganisationId);
    var organisation =
      await _organisationRepository.GetBySpecAsync(organisationSpec, cancellationToken);
    if (organisation == null)
    {
      return Result<OrganisationRole>.NotFound();
    }

    if (organisation.Roles.Exists(r => r.Name == role.Name))
    {
      var error = new ValidationError();
      error.Severity = ValidationSeverity.Error;
      error.Identifier = "Name";
      error.ErrorMessage = "Role already exists";

      return Result.Invalid(new List<ValidationError> { error });
    }

    var created = await _roleRepository.AddAsync(role, cancellationToken);
    await _roleRepository.SaveChangesAsync(cancellationToken);

    organisation.Roles.Add(created);
    await _organisationRepository.UpdateAsync(organisation, cancellationToken);
    await _organisationRepository.SaveChangesAsync(cancellationToken);

    return Result.Success(created);
  }

  public Task<Result> Delete(int organisationRoleId, CancellationToken cancellationToken)
  {
    throw new NotImplementedException();
  }
}
