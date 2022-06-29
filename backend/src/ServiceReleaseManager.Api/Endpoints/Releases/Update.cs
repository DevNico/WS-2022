using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Api.Authorization;
using ServiceReleaseManager.Api.Authorization.Operations.Service;
using ServiceReleaseManager.Core.ReleaseAggregate;
using ServiceReleaseManager.Core.ServiceAggregate;
using ServiceReleaseManager.Core.ServiceAggregate.Specifications;
using ServiceReleaseManager.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.Releases;

public class Update : EndpointBaseAsync.WithRequest<UpdateReleaseRequest>.WithActionResult<
  ReleaseRecord>
{
  private readonly IRepository<Locale> _localeRepository;
  private readonly IRepository<Release> _releaseRepository;
  private readonly IRepository<Service> _serviceRepository;
  private readonly IServiceManagerAuthorizationService _authorizationService;

  public Update(
    IRepository<Release> releaseRepository,
    IRepository<Locale> localeRepository,
    IRepository<Service> serviceRepository,
    IServiceManagerAuthorizationService authorizationService)
  {
    _releaseRepository = releaseRepository;
    _localeRepository = localeRepository;
    _serviceRepository = serviceRepository;
    _authorizationService = authorizationService;
  }

  [HttpPost(UpdateReleaseRequest.Route)]
  [Authorize]
  [SwaggerOperation(
    Summary = "Updates a Release",
    Description = "Updates a Release",
    OperationId = "Release.Update",
    Tags = new[] { "Release" })
  ]
  [SwaggerResponse(StatusCodes.Status200OK, "The Release was created", typeof(ReleaseRecord))]
  [SwaggerResponse(StatusCodes.Status404NotFound, "Release not found")]
  [SwaggerResponse(StatusCodes.Status400BadRequest, "locale id not found")]
  [SwaggerResponse(StatusCodes.Status409Conflict, "Release is approved and cannot be updated")]
  public override async Task<ActionResult<ReleaseRecord>> HandleAsync(
    UpdateReleaseRequest request,
    CancellationToken cancellationToken = new())
  {
    var release = await _releaseRepository.GetByIdAsync(request.ReleaseId, cancellationToken);

    if (release == null)
    {
      return NotFound();
    }

    if (release.ApprovedAt != null)
    {
      return Conflict("Release is approved and cannot be updated");
    }

    var requestLocaleIds = request.LocalisedMetadataList.Select(x => x.LocaleId).ToList();
    if (requestLocaleIds.Count > requestLocaleIds.Distinct().Count())
    {
      return BadRequest("Duplicate locale ids");
    }

    var localesByServiceSpec = new LocalesByServiceIdSpec(release.ServiceId);
    var allLocales = await _localeRepository.ListAsync(cancellationToken);
    var locales = localesByServiceSpec.Evaluate(allLocales);
    var localeById = locales.ToDictionary(l => l.Id);

    if (request.LocalisedMetadataList.Any(localisedMetadata =>
          !localeById.ContainsKey(localisedMetadata.LocaleId)))
    {
      return BadRequest("Locale id not found");
    }

    if (await _authorizationService.EvaluateServiceAuthorization(User, release.ServiceId,
          ReleaseOperations.Release_MetadataEdit, cancellationToken))
    {
      release.Metadata = request.MetaData;
    }

    if (await _authorizationService.EvaluateServiceAuthorization(User, release.ServiceId,
          ReleaseOperations.Release_LocalizedMetadataEdit, cancellationToken))
    {
      updateLocalizedMetadata(request, release, localeById);
    }

    await _releaseRepository.SaveChangesAsync(cancellationToken);

    var response = ReleaseRecord.FromEntity(release);
    return Ok(response);
  }

  private void updateLocalizedMetadata(UpdateReleaseRequest request, Release release,
    Dictionary<int, Locale> localeById)
  {
    foreach (var localisedMetadata in request.LocalisedMetadataList)
    {
      var existing =
        release.LocalisedMetadataList.FirstOrDefault(x => x.LocaleId == localisedMetadata.LocaleId);

      if (existing == null)
      {
        release.LocalisedMetadataList.Add(new ReleaseLocalisedMetadata(
          localisedMetadata.Metadata,
          release,
          localeById[localisedMetadata.LocaleId]
        ));
      }
      else
      {
        existing.Metadata = localisedMetadata.Metadata;
      }
    }
  }
}
