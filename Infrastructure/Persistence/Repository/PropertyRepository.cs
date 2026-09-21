using Application;
using Application.Common.Specifications;
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

    public async Task<IReadOnlyCollection<Property>> SearchAsync(ISpecification<Property> specification, int limit, CancellationToken cancellationToken)
    {
        var query = _dbContext.Properties.AsNoTracking().AsQueryable();
        
        return await query
            .AsNoTracking()
            .Where(specification.ToExpression())
            .OrderByDescending(property => property.ExternalId)
            .Take(limit)
            .ToListAsync(cancellationToken);
    }
}