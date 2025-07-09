using System.Net.Http.Json;
using JET_Task.Blazor.Models;

namespace JET_Task.Blazor.Services
{
    public interface IMovieService
    {
        Task<SearchResultDto?> SearchMoviesAsync(string title);
        Task<MovieDto?> GetMovieByIdAsync(string imdbId);
        Task<IEnumerable<SearchQueryDto>> GetLatestQueriesAsync();
    }

    public class MovieService : IMovieService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<MovieService> _logger;

        public MovieService(HttpClient httpClient, ILogger<MovieService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<SearchResultDto?> SearchMoviesAsync(string title)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/movies/search?title={Uri.EscapeDataString(title)}");
                
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<SearchResultDto>();
                }
                
                _logger.LogWarning("Search request failed with status: {StatusCode}", response.StatusCode);
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching movies for title: {Title}", title);
                return null;
            }
        }

        public async Task<MovieDto?> GetMovieByIdAsync(string imdbId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/movies/{imdbId}");
                
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<MovieDto>();
                }
                
                _logger.LogWarning("Get movie request failed with status: {StatusCode}", response.StatusCode);
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting movie by ID: {ImdbId}", imdbId);
                return null;
            }
        }

        public async Task<IEnumerable<SearchQueryDto>> GetLatestQueriesAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/movies/latest-queries");
                
                if (response.IsSuccessStatusCode)
                {
                    var queries = await response.Content.ReadFromJsonAsync<IEnumerable<SearchQueryDto>>();
                    return queries ?? new List<SearchQueryDto>();
                }
                
                _logger.LogWarning("Get latest queries request failed with status: {StatusCode}", response.StatusCode);
                return new List<SearchQueryDto>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting latest queries");
                return new List<SearchQueryDto>();
            }
        }
    }
} 