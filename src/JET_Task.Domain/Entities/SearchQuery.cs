using System;

namespace JET_Task.Domain.Entities
{
    public class SearchQuery
    {
        public SearchQueryText Query { get; }
        public DateTime Timestamp { get; }
        public int ResultCount { get; }

        // Parameterless constructor for deserialization
        public SearchQuery() { }

        public SearchQuery(SearchQueryText query, int resultCount = 0)
        {
            Query = query ?? throw new ArgumentNullException(nameof(query));
            Timestamp = DateTime.UtcNow;
            ResultCount = resultCount;
        }
    }
} 