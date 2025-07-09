using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using JET_Task.Application.DTOs;
using JET_Task.Domain.Entities;
using JET_Task.Domain.Interfaces;

namespace JET_Task.Application.Services
{
    public interface IMovieService
    {
        Task<SearchResultDto> SearchMoviesAsync(string title);
        Task<MovieDto?> GetMovieByIdAsync(string imdbId);
        Task<IEnumerable<SearchQueryDto>> GetLatestQueriesAsync(int count = 5);
    }

    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly ISearchQueryRepository _searchQueryRepository;
        private readonly IMapper _mapper;

        public MovieService(
            IMovieRepository movieRepository,
            ISearchQueryRepository searchQueryRepository,
            IMapper mapper)
        {
            _movieRepository = movieRepository;
            _searchQueryRepository = searchQueryRepository;
            _mapper = mapper;
        }

        public async Task<SearchResultDto> SearchMoviesAsync(string title)
        {
            var movies = await _movieRepository.SearchMoviesAsync(title);
            var movieDtos = movies.Select(m => _mapper.Map<MovieDto>(m)).ToList();

            var result = new SearchResultDto
            {
                Movies = movieDtos,
                TotalResults = movieDtos.Count,
                Query = title
            };

            // Save the search query
            var searchQuery = new SearchQuery(new SearchQueryText(title), movieDtos.Count);
            await _searchQueryRepository.SaveQueryAsync(searchQuery);

            return result;
        }

        public async Task<MovieDto?> GetMovieByIdAsync(string imdbId)
        {
            var movie = await _movieRepository.GetMovieByIdAsync(imdbId);
            return movie != null ? _mapper.Map<MovieDto>(movie) : null;
        }

        public async Task<IEnumerable<SearchQueryDto>> GetLatestQueriesAsync(int count = 5)
        {
            var queries = await _searchQueryRepository.GetLatestQueriesAsync(count);
            return queries.Select(q => _mapper.Map<SearchQueryDto>(q));
        }
    }
} 