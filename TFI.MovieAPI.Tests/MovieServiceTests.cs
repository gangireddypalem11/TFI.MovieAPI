using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using TFI.MovieAPI.Data;
using TFI.MovieAPI.DTOS;
using TFI.MovieAPI.Models;
using TFI.MovieAPI.Services.Implementations;
using Xunit;

namespace TFI.MovieAPI.Tests
{
    public class MovieServiceTests
    {
        [Fact]
        public async Task GetMovieByIdAsync_WhenMovieExists_ReturnsMovie()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<TFIDbContext>()
                .UseInMemoryDatabase(
                    databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new TFIDbContext(options);

            var director = new Director
            {
                Id = 1,
                Name = "Trivikram",
                Industry = "Telugu"
            };

            context.Directors.Add(director);

            context.Movies.Add(new Movie
            {
                Id = 1,
                Title = "RRR",
                Genre = "Action",
                ReleaseYear = 2022,
                Rating = 9,
                Budget = 500,
                DirectorId = 1
            });

            await context.SaveChangesAsync();

            var logger = new Mock<ILogger<MovieService>>();

            var service = new MovieService(
                context,
                logger.Object);

            // Act
            var result = await service.GetMovieByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("RRR", result.Title);
            Assert.Equal("Action", result.Genre);
            Assert.Equal(2022, result.ReleaseYear);
            Assert.Equal(9, result.Rating);
        }


        [Fact]
        public async Task GetMovieByIdAsync_WhenMovieDoesNotExist_ReturnsNull()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<TFIDbContext>()
                .UseInMemoryDatabase(
                    databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new TFIDbContext(options);

            var logger = new Mock<ILogger<MovieService>>();

            var service = new MovieService(
                context,
                logger.Object);

            // Act
            var result = await service.GetMovieByIdAsync(999);

            // Assert
            Assert.Null(result);
        }


        [Fact]
        public async Task AddMovieAsync_WithValidMovie_ReturnsMovie()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<TFIDbContext>()
                .UseInMemoryDatabase(
                    databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new TFIDbContext(options);

            var logger = new Mock<ILogger<MovieService>>();

            var service = new MovieService(
                context,
                logger.Object);

            var movieDto = new MovieCreateDto
            {
                Title = "Pushpa",
                Genre = "Action",
                ReleaseYear = 2021,
                Rating = 8,
                Budget = 200,
                DirectorId = 1
            };

            // Act
            var result = await service.AddMovieAsync(movieDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Pushpa", result.Title);
            Assert.Equal("Action", result.Genre);
            Assert.Equal(2021, result.ReleaseYear);
            Assert.Equal(8, result.Rating);
        }
        [Fact]
        public async Task UpdateMovieAsync_WhenMovieExists_ReturnsTrue()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<TFIDbContext>()
                .UseInMemoryDatabase(
                    databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new TFIDbContext(options);

            context.Movies.Add(new Movie
            {
                Id = 1,
                Title = "Old Movie",
                Genre = "Drama",
                ReleaseYear = 2020,
                Rating = 7,
                Budget = 100,
                DirectorId = 1
            });

            await context.SaveChangesAsync();

            var logger = new Mock<ILogger<MovieService>>();

            var service = new MovieService(
                context,
                logger.Object);

            var movieDto = new MovieUpdateDto
            {
                Title = "Updated Movie",
                Genre = "Action",
                ReleaseYear = 2024,
                Rating = 9,
                Budget = 200,
                DirectorId = 1
            };

            // Act
            var result = await service.UpdateMovieAsync(1, movieDto);

            // Assert
            Assert.True(result);

            var updatedMovie = await context.Movies.FindAsync(1);

            Assert.Equal("Updated Movie", updatedMovie.Title);
            Assert.Equal("Action", updatedMovie.Genre);
            Assert.Equal(2024, updatedMovie.ReleaseYear);
            Assert.Equal(9, updatedMovie.Rating);
        }
        [Fact]
        public async Task UpdateMovieAsync_WhenMovieDoesNotExist_ReturnsFalse()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<TFIDbContext>()
                .UseInMemoryDatabase(
                    databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new TFIDbContext(options);

            var logger = new Mock<ILogger<MovieService>>();

            var service = new MovieService(
                context,
                logger.Object);

            var movieDto = new MovieUpdateDto
            {
                Title = "Updated Movie",
                Genre = "Action",
                ReleaseYear = 2024,
                Rating = 9,
                Budget = 200,
                DirectorId = 1
            };

            // Act
            var result = await service.UpdateMovieAsync(999, movieDto);

            // Assert
            Assert.False(result);
        }
        [Fact]
        public async Task DeleteMovieAsync_WhenMovieExists_ReturnsTrue()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<TFIDbContext>()
                .UseInMemoryDatabase(
                    databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new TFIDbContext(options);

            context.Movies.Add(new Movie
            {
                Id = 1,
                Title = "RRR",
                Genre = "Action",
                ReleaseYear = 2022,
                Rating = 9,
                Budget = 500,
                DirectorId = 1
            });

            await context.SaveChangesAsync();

            var logger = new Mock<ILogger<MovieService>>();

            var service = new MovieService(
                context,
                logger.Object);

            // Act
            var result = await service.DeleteMovieAsync(1);

            // Assert
            Assert.True(result);

            var deletedMovie = await context.Movies.FindAsync(1);

            Assert.Null(deletedMovie);
        }
        [Fact]
        public async Task DeleteMovieAsync_WhenMovieDoesNotExist_ReturnsFalse()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<TFIDbContext>()
                .UseInMemoryDatabase(
                    databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new TFIDbContext(options);

            var logger = new Mock<ILogger<MovieService>>();

            var service = new MovieService(
                context,
                logger.Object);

            // Act
            var result = await service.DeleteMovieAsync(999);

            // Assert
            Assert.False(result);
        }
    }
}