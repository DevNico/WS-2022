﻿using ServiceReleaseManager.Core.OrganisationAggregate;

namespace ServiceReleaseManager.Api.Endpoints.OrganisationRoles;

public record OrganisationRoleRecord(
  string Name,
  int Id,
  bool ServiceWrite,
  bool ServiceDelete,
  bool UserRead,
  bool UserWrite,
  bool UserDelete
)
{
  public static OrganisationRoleRecord FromEntity(OrganisationRole role)
  {
    return new OrganisationRoleRecord(
      role.Name,
      role.Id,
      role.ServiceWrite,
      role.ServiceDelete,
      role.UserRead,
      role.UserWrite,
      role.UserDelete
    );
  }
}
