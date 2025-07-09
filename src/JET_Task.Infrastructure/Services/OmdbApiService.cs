using System.Text.Json;
using JET_Task.Infrastructure.Models;
using Microsoft.Extensions.Configuration;

namespace JET_Task.Infrastructure.Services
{
    public interface IOmdbApiService
    {
        Task<OmdbSearchResponse?> SearchMoviesAsync(string title);
        Task<OmdbMovieDetail?> GetMovieByIdAsync(string imdbId);
    }

    public class OmdbApiService : IOmdbApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _baseUrl = "http://www.omdbapi.com/";

        public OmdbApiService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["OmdbApi:ApiKey"] ?? string.Empty;
        }

        public async Task<OmdbSearchResponse?> SearchMoviesAsync(string title)
        {
            var url = $"{_baseUrl}?apikey={_apiKey}&s={Uri.EscapeDataString(title)}";
            var response = await _httpClient.GetAsync(url);
            
            if (!response.IsSuccessStatusCode)
                return null;

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<OmdbSearchResponse>(content);
        }

        public async Task<OmdbMovieDetail?> GetMovieByIdAsync(string imdbId)
        {
            var url = $"{_baseUrl}?apikey={_apiKey}&i={imdbId}";
            var response = await _httpClient.GetAsync(url);
            
            if (!response.IsSuccessStatusCode)
                return null;

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<OmdbMovieDetail>(content);
        }
    }
} 