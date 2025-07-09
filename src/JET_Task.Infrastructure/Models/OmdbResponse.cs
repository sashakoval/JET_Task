using System.Text.Json.Serialization;

namespace JET_Task.Infrastructure.Models
{
    public class OmdbSearchResponse
    {
        [JsonPropertyName("Search")]
        public List<OmdbMovie> Search { get; set; } = new();
        
        [JsonPropertyName("totalResults")]
        public string TotalResults { get; set; } = string.Empty;
        
        [JsonPropertyName("Response")]
        public string Response { get; set; } = string.Empty;
    }

    public class OmdbMovie
    {
        [JsonPropertyName("imdbID")]
        public string ImdbId { get; set; } = string.Empty;
        
        [JsonPropertyName("Title")]
        public string Title { get; set; } = string.Empty;
        
        [JsonPropertyName("Year")]
        public string Year { get; set; } = string.Empty;
        
        [JsonPropertyName("Poster")]
        public string Poster { get; set; } = string.Empty;
        
        [JsonPropertyName("Type")]
        public string Type { get; set; } = string.Empty;
    }

    public class OmdbMovieDetail
    {
        [JsonPropertyName("imdbID")]
        public string ImdbId { get; set; } = string.Empty;
        
        [JsonPropertyName("Title")]
        public string Title { get; set; } = string.Empty;
        
        [JsonPropertyName("Year")]
        public string Year { get; set; } = string.Empty;
        
        [JsonPropertyName("Poster")]
        public string Poster { get; set; } = string.Empty;
        
        [JsonPropertyName("Plot")]
        public string Plot { get; set; } = string.Empty;
        
        [JsonPropertyName("imdbRating")]
        public string ImdbRating { get; set; } = string.Empty;
        
        [JsonPropertyName("Director")]
        public string Director { get; set; } = string.Empty;
        
        [JsonPropertyName("Actors")]
        public string Actors { get; set; } = string.Empty;
        
        [JsonPropertyName("Genre")]
        public string Genre { get; set; } = string.Empty;
        
        [JsonPropertyName("Runtime")]
        public string Runtime { get; set; } = string.Empty;
        
        [JsonPropertyName("Type")]
        public string Type { get; set; } = string.Empty;
        
        [JsonPropertyName("Response")]
        public string Response { get; set; } = string.Empty;
    }
} 