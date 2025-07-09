namespace JET_Task.Infrastructure.Models
{
    public class SearchQueryDto
    {
        public string Query { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
        public int ResultCount { get; set; }

        public SearchQueryDto() { }

        public SearchQueryDto(string query, int resultCount)
        {
            Query = query;
            Timestamp = DateTime.UtcNow;
            ResultCount = resultCount;
        }
    }
} 