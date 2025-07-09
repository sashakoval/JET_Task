using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using JET_Task.Domain.Entities;
using JET_Task.Domain.Interfaces;
using JET_Task.Infrastructure.Models;
using StackExchange.Redis;

namespace JET_Task.Infrastructure.Repositories
{
    public class RedisSearchQueryRepository : ISearchQueryRepository
    {
        private readonly IConnectionMultiplexer _redis;
        private const string Key = "latest_search_queries";
        private const int MaxQueries = 5;

        public RedisSearchQueryRepository(IConnectionMultiplexer redis)
        {
            _redis = redis;
        }

        public async Task<IEnumerable<SearchQuery>> GetLatestQueriesAsync(int count)
        {
            var db = _redis.GetDatabase();
            var queries = await db.ListRangeAsync(Key, 0, count - 1);
            
            var result = new List<SearchQuery>();
            foreach (var queryJson in queries)
            {
                if (!string.IsNullOrEmpty(queryJson))
                {
                    var queryDto = JsonSerializer.Deserialize<SearchQueryDto>(queryJson.ToString());
                    if (queryDto != null && !string.IsNullOrEmpty(queryDto.Query))
                    {
                        var searchQuery = new SearchQuery(new SearchQueryText(queryDto.Query), queryDto.ResultCount);
                        result.Add(searchQuery);
                    }
                }
            }
            
            return result;
        }

        public async Task SaveQueryAsync(SearchQuery query)
        {
            var db = _redis.GetDatabase();
            var queryDto = new SearchQueryDto(query.Query.Value, query.ResultCount);
            var queryJson = JsonSerializer.Serialize(queryDto);
            
            // Add to the beginning of the list
            await db.ListLeftPushAsync(Key, queryJson);
            
            // Keep only the latest MaxQueries items
            await db.ListTrimAsync(Key, 0, MaxQueries - 1);
        }
    }
} 