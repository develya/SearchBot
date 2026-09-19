using Application.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repository;

public class SearchRequestRepository : ISearchRequestRepository
{
    private readonly AppDbContext _dbContext;
    public SearchRequestRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<SearchRequest> AddAsync(SearchRequest request, CancellationToken cancellationToken)
    {
        if (request.Id == Guid.Empty)
        {
            request.Id = Guid.NewGuid();
        }
        await _dbContext.SearchRequests.AddAsync(request, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return request;
    }
    public async Task<IReadOnlyCollection<SearchRequest>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _dbContext.SearchRequests
            .Where(r => r.UserId == userId)
            .ToListAsync(cancellationToken);
    }
    public async Task<SearchRequest?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.SearchRequests
            .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
    }
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var request = await _dbContext.SearchRequests
            .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
        if (request is null)
        {
            return false;
        }
        _dbContext.SearchRequests.Remove(request);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
}