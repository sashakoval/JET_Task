using JET_Task.Domain.Entities;

namespace JET_Task.Domain.Interfaces
{
    public interface IMovieRepository
    {
        Task<IEnumerable<Movie>> SearchMoviesAsync(string title);
        Task<Movie?> GetMovieByIdAsync(string imdbId);
    }
} 