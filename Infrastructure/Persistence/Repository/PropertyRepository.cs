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
}