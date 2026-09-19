using Application;
using Application.Interfaces;
using Domain;
using Infrastructure.DomRia;
using Infrastructure.DomRia.Mapping;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services;

public class PropertySyncService : IPropertySyncService
{
    private const int MaxNewPropertiesPerCity = 10;
    private const int RequestDelayMs = 300;

    private readonly DomRiaClient _domRiaClient;
    private readonly DomRiaPropertyEntityMapper _entityMapper;
    private readonly IPropertyRepository _propertyRepository;
    private readonly AppDbContext _dbContext;
    private readonly ILogger<PropertySyncService> _logger;

    public PropertySyncService(
        DomRiaClient domRiaClient,
        DomRiaPropertyEntityMapper entityMapper,
        IPropertyRepository propertyRepository,
        AppDbContext dbContext,
        ILogger<PropertySyncService> logger)
    {
        _domRiaClient = domRiaClient;
        _entityMapper = entityMapper;
        _propertyRepository = propertyRepository;
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task SyncPropertiesAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting background property synchronization...");

        var cityIds = await GetTargetCityIdsAsync(cancellationToken);

        foreach (var cityId in cityIds)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                break;
            }

            try
            {
                await SyncCityPropertiesAsync(cityId, cancellationToken);
            }
            catch (PropertyProviderException ex) when (ex.UpstreamStatusCode == 429)
            {
                _logger.LogWarning("DomRia rate limit exceeded (429) while syncing CityId {CityId}. Backing off.", cityId);
                break;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error syncing properties for CityId {CityId}.", cityId);
            }
        }

        _logger.LogInformation("Background property synchronization finished.");
    }

    private async Task<List<int>> GetTargetCityIdsAsync(CancellationToken cancellationToken)
    {
        var activeCityIdStrings = await _dbContext.SearchRequests
            .Select(r => r.CityId)
            .Distinct()
            .ToListAsync(cancellationToken);

        var result = new List<int>();

        foreach (var str in activeCityIdStrings)
        {
            if (int.TryParse(str, out var id) && !result.Contains(id))
            {
                result.Add(id);
            }
        }

        if (result.Count == 0)
        {
            result.AddRange([
                1,  // Вінниця
                10, // Київ
                5,  // Львів
                12, // Одеса
                11  // Дніпро
            ]);
        }

        return result;
    }

    private async Task SyncCityPropertiesAsync(int cityId, CancellationToken cancellationToken)
    {
        var searchRequest = new PropertySearchRequest { CityId = cityId };
        var ids = await _domRiaClient.GetSearchIdsAsync(searchRequest, cancellationToken);

        if (ids.Count == 0)
        {
            return;
        }

        var existingProperties = await _propertyRepository.GetByExternalIdsAsync(ids, cancellationToken);
        var existingExternalIds = existingProperties
            .Select(p => p.ExternalId)
            .ToHashSet();

        var missingIds = ids
            .Where(id => !existingExternalIds.Contains(id))
            .Take(MaxNewPropertiesPerCity)
            .ToList();

        if (missingIds.Count == 0)
        {
            _logger.LogInformation("All properties for CityId {CityId} are already up to date.", cityId);
            return;
        }

        _logger.LogInformation("Found {Count} new properties to sync for CityId {CityId}.", missingIds.Count, cityId);

        var newProperties = new List<Property>();

        foreach (var id in missingIds)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                break;
            }

            var domRiaProperty = await _domRiaClient.GetPropertyByIdAsync(id, cancellationToken);

            if (domRiaProperty is not null)
            {
                var property = _entityMapper.Map(domRiaProperty);
                newProperties.Add(property);
            }

            await Task.Delay(RequestDelayMs, cancellationToken);
        }

        if (newProperties.Count > 0)
        {
            await _propertyRepository.AddRangeAsync(newProperties, cancellationToken);
            _logger.LogInformation("Saved {Count} new properties to database for CityId {CityId}.", newProperties.Count, cityId);
        }
    }
}
