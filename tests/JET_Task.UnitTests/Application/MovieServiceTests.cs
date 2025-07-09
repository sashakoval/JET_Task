using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JET_Task.Application.DTOs;
using JET_Task.Application.Services;
using JET_Task.Application.Mapping;
using JET_Task.Domain.Entities;
using JET_Task.Domain.Interfaces;
using Moq;
using Xunit;
using AutoMapper;

namespace JET_Task.UnitTests.Application
{
    public class MovieServiceTests
    {
        private readonly Mock<IMovieRepository> _mockMovieRepository;
        private readonly Mock<ISearchQueryRepository> _mockSearchQueryRepository;
        private readonly IMapper _mapper;
        private readonly IMovieService _movieService;

        public MovieServiceTests()
        {
            _mockMovieRepository = new Mock<IMovieRepository>();
            _mockSearchQueryRepository = new Mock<ISearchQueryRepository>();
            
            // Configure AutoMapper for tests
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ApplicationMappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            _movieService = new MovieService(_mockMovieRepository.Object, _mockSearchQueryRepository.Object, _mapper);
        }

        [Fact]
        public async Task SearchMoviesAsync_ShouldReturnSearchResult_WhenMoviesFound()
        {
            // Arrange
            var searchTitle = "Batman";
            var movies = new List<Movie>
            {
                new Movie(new MovieId("tt0372784"), "Batman Begins", "2005", "poster1.jpg"),
                new Movie(new MovieId("tt0468569"), "The Dark Knight", "2008", "poster2.jpg")
            };

            _mockMovieRepository.Setup(x => x.SearchMoviesAsync(searchTitle))
                .ReturnsAsync(movies);

            _mockSearchQueryRepository.Setup(x => x.SaveQueryAsync(It.IsAny<SearchQuery>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _movieService.SearchMoviesAsync(searchTitle);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(searchTitle, result.Query);
            Assert.Equal(2, result.TotalResults);
            Assert.Equal(2, result.Movies.Count());
        }

        [Fact]
        public async Task GetMovieByIdAsync_ShouldReturnMovie_WhenMovieExists()
        {
            // Arrange
            var imdbId = "tt0372784";
            var movie = new Movie(
                new MovieId(imdbId),
                "Batman Begins",
                "2005",
                "poster.jpg",
                "A great movie",
                "8.3",
                "Christopher Nolan",
                "Christian Bale",
                "Action",
                "140 min",
                "movie");

            _mockMovieRepository.Setup(x => x.GetMovieByIdAsync(imdbId))
                .ReturnsAsync(movie);

            // Act
            var result = await _movieService.GetMovieByIdAsync(imdbId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(imdbId, result.ImdbId);
            Assert.Equal("Batman Begins", result.Title);
            Assert.Equal("2005", result.Year);
        }

        [Fact]
        public async Task GetLatestQueriesAsync_ShouldReturnQueries()
        {
            // Arrange
            var queries = new List<SearchQuery>
            {
                new SearchQuery(new SearchQueryText("Batman"), 5),
                new SearchQuery(new SearchQueryText("Superman"), 3)
            };

            _mockSearchQueryRepository.Setup(x => x.GetLatestQueriesAsync(5))
                .ReturnsAsync(queries);

            // Act
            var result = await _movieService.GetLatestQueriesAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }
    }
} 