using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using JET_Task.Domain.Entities;
using JET_Task.Domain.Interfaces;
using JET_Task.Infrastructure.Models;
using JET_Task.Infrastructure.Services;

namespace JET_Task.Infrastructure.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly IOmdbApiService _omdbApiService;
        private readonly IMapper _mapper;

        public MovieRepository(IOmdbApiService omdbApiService, IMapper mapper)
        {
            _omdbApiService = omdbApiService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Movie>> SearchMoviesAsync(string title)
        {
            var response = await _omdbApiService.SearchMoviesAsync(title);
            
            if (response?.Search == null)
                return new List<Movie>();

            return _mapper.Map<IEnumerable<Movie>>(response.Search);
        }

        public async Task<Movie?> GetMovieByIdAsync(string imdbId)
        {
            var response = await _omdbApiService.GetMovieByIdAsync(imdbId);
            
            if (response == null || response.Response != "True")
                return null;

            return _mapper.Map<Movie>(response);
        }
    }
} 