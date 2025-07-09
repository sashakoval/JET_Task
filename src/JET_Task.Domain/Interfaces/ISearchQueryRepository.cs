using JET_Task.Domain.Entities;

namespace JET_Task.Domain.Interfaces
{
    public interface ISearchQueryRepository
    {
        Task<IEnumerable<SearchQuery>> GetLatestQueriesAsync(int count);
        Task SaveQueryAsync(SearchQuery query);
    }
} 