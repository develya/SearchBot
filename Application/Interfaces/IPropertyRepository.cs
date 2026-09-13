using Domain;

namespace Application.Interfaces;

public interface IPropertyRepository
{
    Task<Property?> GetByExternalIdAsync(int externalId, CancellationToken cancellationToken);
    Task AddAsync(Property property, CancellationToken cancellationToken);
}