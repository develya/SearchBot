using Domain;

namespace Application.Interfaces;

public interface ISearchRequestRepository
{
    Task<SearchRequest> AddAsync(SearchRequest request, CancellationToken cancellationToken);
    Task<IReadOnlyCollection<SearchRequest>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken);
    Task<SearchRequest?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
}