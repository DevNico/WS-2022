using Ardalis.Result;
using ServiceReleaseManager.Core.Interfaces;
using ServiceReleaseManager.Core.OrganisationAggregate;
using ServiceReleaseManager.Core.OrganisationAggregate.Specifications;
using ServiceReleaseManager.SharedKernel.Interfaces;

namespace ServiceReleaseManager.Core.Services;

public class OrganisationRoleService : IOrganisationRoleService
{
  private readonly IRepository<Organisation> _organisationRepository;
  private readonly IRepository<OrganisationRole> _roleRepository;
  private readonly IRepository<OrganisationUser> _userRepository;

  public OrganisationRoleService(
    IRepository<Organisation> organisationRepository,
    IRepository<OrganisationRole> roleRepository,
    IRepository<OrganisationUser> userRepository
  )
  {
    _organisationRepository = organisationRepository;
    _roleRepository = roleRepository;
    _userRepository = userRepository;
  }

  public async Task<Result<List<OrganisationRole>>> ListByOrganisationRouteName(
    string organisationRouteName,
    CancellationToken cancellationToken
  )
  {
    var spec = new OrganisationRolesByOrganisationRouteNameSpec(organisationRouteName);
    var roles =
      await _organisationRepository.GetBySpecAsync<IEnumerable<OrganisationRole>>(
        spec,
        cancellationToken
      );

    return Result.Success((roles ?? new List<OrganisationRole>()).ToList());
  }

  public async Task<Result<OrganisationRole>> Create(
    OrganisationRole role,
    CancellationToken cancellationToken
  )
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
      return Result.Invalid(
        new List<ValidationError>
        {
          new()
          {
            Severity = ValidationSeverity.Error,
            Identifier = "Name",
            ErrorMessage = "Role already exists"
          }
        }
      );
    }

    var created = await _roleRepository.AddAsync(role, cancellationToken);
    await _roleRepository.SaveChangesAsync(cancellationToken);

    organisation.Roles.Add(created);
    await _organisationRepository.UpdateAsync(organisation, cancellationToken);
    await _organisationRepository.SaveChangesAsync(cancellationToken);

    return Result.Success(created);
  }

  public async Task<Result> Delete(int organisationRoleId, CancellationToken cancellationToken)
  {
    var spec = new OrganisationUsersByRoleIdSpec(organisationRoleId);
    if (await _userRepository.CountAsync(spec, cancellationToken) > 0)
    {
      return Result.Invalid(new List<ValidationError>());
    }

    var roleSpec = new OrganisationRoleByIdSpec(organisationRoleId);
    var role = await _roleRepository.GetBySpecAsync(roleSpec, cancellationToken);
    if (role == null)
    {
      return Result.NotFound();
    }

    var organisationSpec = new OrganisationByIdSpec(role.OrganisationId);
    var organisation =
      await _organisationRepository.GetBySpecAsync(organisationSpec, cancellationToken);
    if (organisation == null)
    {
      return Result.NotFound();
    }

    var found = organisation.Roles.Find(r => r.Id == organisationRoleId);
    if (found == null)
    {
      return Result.NotFound();
    }

    organisation.Roles.Remove(found);
    await _organisationRepository.UpdateAsync(organisation, cancellationToken);
    await _organisationRepository.SaveChangesAsync(cancellationToken);

    await _roleRepository.DeleteAsync(role, cancellationToken);
    await _roleRepository.SaveChangesAsync(cancellationToken);

    return Result.Success();
  }
}
