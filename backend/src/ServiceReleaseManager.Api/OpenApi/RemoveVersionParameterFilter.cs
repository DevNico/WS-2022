using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ServiceReleaseManager.Api.OpenApi;

internal class RemoveVersionParameterFilter : IOperationFilter
{
  public void Apply(OpenApiOperation operation, OperationFilterContext context)
  {
    if (operation.Parameters.All(p => p.Name != "version"))
    {
      return;
    }

    var versionParameter = operation.Parameters.Single(p => p.Name == "version");
    operation.Parameters.Remove(versionParameter);
  }
}
