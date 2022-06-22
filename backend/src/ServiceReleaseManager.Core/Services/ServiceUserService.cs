using ServiceReleaseManager.Core.ServiceAggregate;
using ServiceReleaseManager.Core.ServiceAggregate.Specifications;
using ServiceReleaseManager.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceReleaseManager.Core.Services;

public class ServiceUserService
{
  private readonly IRepository<ServiceUser> _repository;

  public ServiceUserService(IRepository<ServiceUser> repository)
  {
    _repository = repository;
  }

  public async Task<ServiceUser?> getByOrganisationUserId(int organisationUserId, CancellationToken cancellationToken)
  {
    var spec = new ServiceUserByOrganisationUserIdSpec(organisationUserId);
    return await _repository.GetBySpecAsync(spec, cancellationToken);
  }



}
