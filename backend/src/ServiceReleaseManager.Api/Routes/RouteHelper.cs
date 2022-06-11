namespace ServiceReleaseManager.Api.Routes;

public static class RouteHelper
{
  private const string _organisationNameAttr = "{OrganisationName}";
  private const string _roleIdAttr = "{RoleId:int}";
  private const string _orgUserIdAttr = "{OrgUserId:int}";

  private const string _organisationsBaseRoute = "/organisations";

  public const string Organisations_Create = $"{_organisationsBaseRoute}";
  public const string Organizations_Delete = $"{_organisationsBaseRoute}/{_organisationNameAttr}";
  public const string Organizations_GetById = $"{_organisationsBaseRoute}/{_organisationNameAttr}";
  public const string Organizations_List = $"{_organisationsBaseRoute}";

  private const string _organisationRolesBaseRoute =
    $"{_organisationsBaseRoute}/{_organisationNameAttr}/roles";

  public const string OrganizationRoles_Create = $"{_organisationRolesBaseRoute}";
  public const string OrganizationRoles_Delete = $"{_organisationRolesBaseRoute}/{_roleIdAttr}";
  public const string OrganizationRoles_GetByName = $"{_organisationRolesBaseRoute}/{_roleIdAttr}";
  public const string OrganizationRoles_List = $"{_organisationRolesBaseRoute}";

  private const string _organisationUsersBaseRoute =
    $"{_organisationsBaseRoute}/{_organisationNameAttr}/users";

  public const string OrganizationUsers_Create = $"{_organisationUsersBaseRoute}";
  public const string OrganizationUsers_Delete = $"{_organisationUsersBaseRoute}/{_orgUserIdAttr}";
  public const string OrganizationUsers_GetById = $"{_organisationUsersBaseRoute}/{_orgUserIdAttr}";
  public const string OrganizationUsers_List = $"{_organisationUsersBaseRoute}";

  public static string ReplaceOrganisationNameAttr(this string routeStr, string organisationName)
  {
    return routeStr.Replace(_organisationNameAttr, organisationName);
  }

  public static string ReplaceRoleIdAttr(this string routeStr, string roleId)
  {
    return routeStr.Replace(_roleIdAttr, roleId);
  }

  public static string ReplaceOrgUserIdAttr(this string routeStr, int userId)
  {
    return routeStr.Replace(_orgUserIdAttr, userId.ToString());
  }
}
