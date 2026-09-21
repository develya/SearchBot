using Domain;
using Application.Common.Specifications;

namespace Application.Interfaces;

public interface IPropertyRepository
{
    Task<Property?> GetByExternalIdAsync(int externalId, CancellationToken cancellationToken);
    Task<IReadOnlyCollection<Property>> GetByExternalIdsAsync(IEnumerable<int> externalIds, CancellationToken cancellationToken);
    Task<IReadOnlyCollection<Property>> SearchAsync(ISpecification<Property> specification, int limit, CancellationToken cancellationToken);
    Task AddAsync(Property property, CancellationToken cancellationToken);
    Task AddRangeAsync(IEnumerable<Property> properties, CancellationToken cancellationToken);
}