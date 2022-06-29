using Ardalis.ApiEndpoints;
using Ardalis.Specification;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Api.Authorization;
using ServiceReleaseManager.Api.Authorization.Operations.Service;
using ServiceReleaseManager.Api.Endpoints.ReleaseEndpoints;
using ServiceReleaseManager.Core.ReleaseAggregate;
using ServiceReleaseManager.Core.ReleaseAggregate.Specifications;
using ServiceReleaseManager.Core.ServiceAggregate;
using ServiceReleaseManager.Core.ServiceAggregate.Specifications;
using ServiceReleaseManager.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.Releases;

public class Create : EndpointBaseAsync.WithRequest<CreateReleaseRequest>.WithActionResult<
  ReleaseRecord>
{
  private readonly IRepository<Locale> _localeRepository;
  private readonly IRepository<Release> _releaseRepository;
  private readonly IRepository<Service> _serviceRepository;
  private readonly IServiceManagerAuthorizationService _authorizationService;

  public Create(
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

  [HttpPost(CreateReleaseRequest.Route)]
  [Authorize]
  [SwaggerOperation(
    Summary = "Creates a new Release",
    Description = "Creates a new Release",
    OperationId = "Release.Create",
    Tags = new[] { "Release" })
  ]
  [SwaggerResponse(StatusCodes.Status200OK, "The Release was created", typeof(ReleaseRecord))]
  [SwaggerResponse(StatusCodes.Status400BadRequest, "Service or locale id not found")]
  [SwaggerResponse(StatusCodes.Status409Conflict,
    "There already is an active release for this service")]
  public override async Task<ActionResult<ReleaseRecord>> HandleAsync(
    CreateReleaseRequest request,
    CancellationToken cancellationToken = new())
  {
    var service = await _serviceRepository.GetByIdAsync(request.ServiceId, cancellationToken);

    // TODO: Service Authorization
    if (service == null)
    {
      return BadRequest("Service id not found");
    }

    if (!await _authorizationService.EvaluateServiceAuthorization(User, request.ServiceId,
          ReleaseOperations.Release_Create, cancellationToken))
    {
      return Unauthorized();
    }

    var activeReleaseSpec = new ActiveReleaseByServiceIdSpec(service.Id);
    var activeRelease =
      await _releaseRepository.GetBySpecAsync(activeReleaseSpec, cancellationToken);

    if (activeRelease != null)
    {
      return Conflict("There already is an active release for this service");
    }

    var requestLocaleIds = request.LocalisedMetadataList.Select(x => x.LocaleId).ToList();
    if (requestLocaleIds.Count > requestLocaleIds.Distinct().Count())
    {
      return BadRequest("Duplicate locale ids");
    }

    var localesByServiceSpec = new LocalesByServiceIdSpec(request.ServiceId);
    var allLocales = await _localeRepository.ListAsync(cancellationToken);
    var locales = localesByServiceSpec.Evaluate(allLocales);
    var localeById = locales.ToDictionary(l => l.Id);

    if (request.LocalisedMetadataList.Any(localisedMetadata =>
          !localeById.ContainsKey(localisedMetadata.LocaleId)))
    {
      return BadRequest("Locale id not found");
    }

    var newRelease = new Release(
      request.Version,
      request.MetaData,
      service.Id
    );
    var createdRelease = await _releaseRepository.AddAsync(newRelease, cancellationToken);

    createdRelease.LocalisedMetadataList = request.LocalisedMetadataList.Select(
      metadata => new ReleaseLocalisedMetadata(
        metadata.Metadata,
        createdRelease,
        localeById[metadata.LocaleId]
      )
    ).ToList();
    await _releaseRepository.SaveChangesAsync(cancellationToken);

    var response = ReleaseRecord.FromEntity(createdRelease);
    return Ok(response);
  }
}
