using JET_Task.Application.DTOs;
using JET_Task.Application.Services;

namespace JET_Task.Api.Endpoints
{
    public static class MovieEndpoints
    {
        public static void MapMovieEndpoints(this WebApplication app)
        {
            // Health check endpoint
            app.MapGet("/health", () => Results.Ok(new { status = "healthy", timestamp = DateTime.UtcNow }))
                .WithName("HealthCheck")
                .WithSummary("Health check endpoint")
                .WithDescription("Returns the health status of the API");

            var group = app.MapGroup("/api/movies")
                .WithTags("Movies")
                .WithOpenApi();

            // Search movies by title
            group.MapGet("/search", async (
                string title,
                IMovieService movieService) =>
            {
                if (string.IsNullOrWhiteSpace(title))
                    return Results.BadRequest(new { error = "Title is required" });

                var result = await movieService.SearchMoviesAsync(title);
                return Results.Ok(result);
            })
            .WithName("SearchMovies")
            .WithSummary("Search movies by title")
            .WithDescription("Searches for movies using the provided title and returns matching results")
            .Produces<SearchResultDto>(200)
            .Produces(400);

            // Get movie by IMDb ID
            group.MapGet("/{imdbId}", async (
                string imdbId,
                IMovieService movieService) =>
            {
                if (string.IsNullOrWhiteSpace(imdbId))
                    return Results.BadRequest(new { error = "IMDb ID is required" });

                var movie = await movieService.GetMovieByIdAsync(imdbId);
                
                if (movie == null)
                    return Results.NotFound(new { error = "Movie not found" });

                return Results.Ok(movie);
            })
            .WithName("GetMovieById")
            .WithSummary("Get movie details by IMDb ID")
            .WithDescription("Retrieves detailed information about a specific movie using its IMDb ID")
            .Produces<MovieDto>(200)
            .Produces(400)
            .Produces(404);

            // Get latest search queries
            group.MapGet("/latest-queries", async (IMovieService movieService) =>
            {
                var queries = await movieService.GetLatestQueriesAsync();
                return Results.Ok(queries);
            })
            .WithName("GetLatestQueries")
            .WithSummary("Get latest search queries")
            .WithDescription("Retrieves the 5 most recent search queries with their result counts")
            .Produces<IEnumerable<SearchQueryDto>>(200);
        }
    }
} 