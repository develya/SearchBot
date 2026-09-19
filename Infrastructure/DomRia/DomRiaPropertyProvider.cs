using Application;
using Application.Interfaces;
using Infrastructure.DomRia.Mapping;

namespace Infrastructure.DomRia;

public class DomRiaPropertyProvider : IPropertyProvider
{
    private readonly DomRiaClient _domRiaClient;
    private readonly DomRiaPropertyEntityMapper _entityMapper;
    private readonly PropertyDtoMapper _dtoMapper;
    private readonly IPropertyRepository _propertyRepository;

    public DomRiaPropertyProvider(DomRiaClient domRiaClient, DomRiaPropertyEntityMapper entityMapper, PropertyDtoMapper dtoMapper, IPropertyRepository propertyRepository)
    {
        _domRiaClient = domRiaClient;
        _entityMapper = entityMapper;
        _dtoMapper = dtoMapper;
        _propertyRepository = propertyRepository;
    }

    private const int TargetCount = 10;

    public async Task<IReadOnlyCollection<PropertyDto>> SearchAsync(PropertySearchRequest request, CancellationToken cancellationToken)
    {
        var localProperties = (await _propertyRepository.SearchAsync(request, TargetCount, cancellationToken)).ToList();

        if (localProperties.Count > 0)
        {
            return localProperties
                .Select(_dtoMapper.Map)
                .ToArray();
        }

        var neededCount = TargetCount;

        try
        {
            var ids = await _domRiaClient.GetSearchIdsAsync(request, cancellationToken);

            if (ids.Count > 0)
            {
                var localExternalIds = localProperties
                    .Select(property => property.ExternalId)
                    .ToHashSet();

                var existingProperties = await _propertyRepository.GetByExternalIdsAsync(ids, cancellationToken);
                var existingExternalIds = existingProperties
                    .Select(property => property.ExternalId)
                    .ToHashSet();

                var missingIds = ids
                    .Where(id => !existingExternalIds.Contains(id))
                    .Take(neededCount)
                    .ToList();

                var newProperties = new List<Domain.Property>();

                foreach (var id in missingIds)
                {
                    var domRiaProperty = await _domRiaClient.GetPropertyByIdAsync(id, cancellationToken);

                    if (domRiaProperty is null)
                    {
                        continue;
                    }

                    var property = _entityMapper.Map(domRiaProperty);
                    newProperties.Add(property);
                }

                if (newProperties.Count > 0)
                {
                    await _propertyRepository.AddRangeAsync(newProperties, cancellationToken);
                }

                var combined = localProperties
                    .Concat(existingProperties.Where(p => !localExternalIds.Contains(p.ExternalId)))
                    .Concat(newProperties)
                    .DistinctBy(p => p.ExternalId)
                    .Take(TargetCount)
                    .ToList();

                return combined
                    .Select(_dtoMapper.Map)
                    .ToArray();
            }
        }
        catch (PropertyProviderException ex) when (ex.UpstreamStatusCode == 429 && localProperties.Count > 0)
        {
            return localProperties
                .Select(_dtoMapper.Map)
                .ToArray();
        }

        return localProperties
            .Select(_dtoMapper.Map)
            .ToArray();
    }
}
