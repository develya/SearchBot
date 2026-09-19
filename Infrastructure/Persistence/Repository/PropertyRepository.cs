using Application;
using Application.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repository;

public class PropertyRepository : IPropertyRepository
{
    private readonly AppDbContext _dbContext;

    public PropertyRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Property?> GetByExternalIdAsync(int externalId, CancellationToken cancellationToken)
    {
        return await _dbContext.Properties.FirstOrDefaultAsync(property => property.ExternalId == externalId, cancellationToken);
    }
    public async Task AddAsync(Property property, CancellationToken cancellationToken)
    {
        await _dbContext.Properties.AddAsync(property, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task AddRangeAsync(IEnumerable<Property> properties, CancellationToken cancellationToken)
    {
        await _dbContext.Properties.AddRangeAsync(properties, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
    
    public async Task<IReadOnlyCollection<Property>> GetByExternalIdsAsync(IEnumerable<int> externalIds, CancellationToken cancellationToken)
    {
        return await _dbContext.Properties
            .Where(property => externalIds.Contains(property.ExternalId))
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<Property>> SearchAsync(PropertySearchRequest request, int limit, CancellationToken cancellationToken)
    {
        var query = _dbContext.Properties.AsNoTracking().AsQueryable();

        if (request.CityId.HasValue)
        {
            query = query.Where(property => property.CityId == request.CityId.Value);
        }

        if (request.MinPrice.HasValue)
        {
            query = query.Where(property => property.Price >= request.MinPrice.Value);
        }

        if (request.MaxPrice.HasValue)
        {
            query = query.Where(property => property.Price <= request.MaxPrice.Value);
        }

        if (request.MinRooms.HasValue)
        {
            query = query.Where(property => property.Rooms >= request.MinRooms.Value);
        }

        if (request.MaxRooms.HasValue)
        {
            query = query.Where(property => property.Rooms <= request.MaxRooms.Value);
        }
        
        if (request.MinFloor.HasValue)
        {
            query = query.Where(property => property.Floor >= request.MinFloor.Value);
        }

        if (request.MaxFloor.HasValue)
        {
            query = query.Where(property => property.Floor <= request.MaxFloor.Value);
        }

        return await query
            .OrderByDescending(property => property.ExternalId)
            .Take(limit)
            .ToListAsync(cancellationToken);
    }
}