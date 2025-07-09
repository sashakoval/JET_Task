using AutoMapper;
using JET_Task.Domain.Entities;
using JET_Task.Infrastructure.Models;

namespace JET_Task.Infrastructure.Mapping
{
    public class MovieMappingProfile : Profile
    {
        public MovieMappingProfile()
        {
            // Map OmdbMovie to Movie (for search results)
            CreateMap<OmdbMovie, Movie>()
                .ConstructUsing((src, ctx) => new Movie(
                    new MovieId(src.ImdbId),
                    src.Title,
                    src.Year,
                    src.Poster,
                    "", // Plot
                    "", // ImdbRating
                    "", // Director
                    "", // Actors
                    "", // Genre
                    "", // Runtime
                    src.Type));

            // Map OmdbMovieDetail to Movie (for detailed movie info)
            CreateMap<OmdbMovieDetail, Movie>()
                .ConstructUsing((src, ctx) => new Movie(
                    new MovieId(src.ImdbId),
                    src.Title,
                    src.Year,
                    src.Poster,
                    src.Plot,
                    src.ImdbRating,
                    src.Director,
                    src.Actors,
                    src.Genre,
                    src.Runtime,
                    src.Type,
                    DateTime.UtcNow));
        }
    }
} 