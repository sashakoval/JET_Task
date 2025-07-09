namespace JET_Task.Blazor.Models
{
    public class MovieDto
    {
        public string ImdbId { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Year { get; set; } = string.Empty;
        public string Poster { get; set; } = string.Empty;
        public string Plot { get; set; } = string.Empty;
        public string ImdbRating { get; set; } = string.Empty;
        public string Director { get; set; } = string.Empty;
        public string Actors { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public string Runtime { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
    }

    public class SearchResultDto
    {
        public IEnumerable<MovieDto> Movies { get; set; } = new List<MovieDto>();
        public int TotalResults { get; set; }
        public string Query { get; set; } = string.Empty;
    }

    public class SearchQueryDto
    {
        public string Query { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
        public int ResultCount { get; set; }
    }
} 