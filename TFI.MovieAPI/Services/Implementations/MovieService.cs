using Microsoft.EntityFrameworkCore;
using TFI.MovieAPI.Data;
using TFI.MovieAPI.DTOS;
using TFI.MovieAPI.Models;
using TFI.MovieAPI.Services.Interfaces;

namespace TFI.MovieAPI.Services.Implementations
{
    public class MovieService : IMovieService
    {
        private readonly TFIDbContext _context;
        private readonly ILogger<MovieService> _logger;

        public MovieService(
            TFIDbContext context,
            ILogger<MovieService> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: All Movies
        public async Task<List<MovieResponseDto>> GetMoviesAsync()
        {
            _logger.LogInformation("Fetching all movies");

            var movies = await _context.Movies
                .Include(m => m.Director)
                .ToListAsync();

            var response = movies.Select(movie => new MovieResponseDto
            {
                Id = movie.Id,
                Title = movie.Title,
                Genre = movie.Genre,
                ReleaseYear = movie.ReleaseYear,
                Rating = movie.Rating
            }).ToList();

            _logger.LogInformation(
                "Retrieved {Count} movies",
                response.Count);

            return response;
        }

        // GET: Movie By Id
        public async Task<MovieResponseDto?> GetMovieByIdAsync(int id)
        {
            _logger.LogInformation(
                "Fetching movie with Id {MovieId}",
                id);

            var movie = await _context.Movies
                .Include(m => m.Director)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movie == null)
            {
                _logger.LogWarning(
                    "Movie with Id {MovieId} was not found",
                    id);

                return null;
            }

            var response = new MovieResponseDto
            {
                Id = movie.Id,
                Title = movie.Title,
                Genre = movie.Genre,
                ReleaseYear = movie.ReleaseYear,
                Rating = movie.Rating
            };

            _logger.LogInformation(
                "Movie with Id {MovieId} retrieved successfully",
                id);

            return response;
        }

        // POST: Add Movie
        public async Task<MovieResponseDto> AddMovieAsync(
            MovieCreateDto movieDto)
        {
            _logger.LogInformation(
     "Adding movie with title {Title}",
     movieDto.Title);

            var movie = new Movie
            {
                Title = movieDto.Title,
                Genre = movieDto.Genre,
                ReleaseYear = movieDto.ReleaseYear,
                Rating = movieDto.Rating,
                Budget = movieDto.Budget,
                DirectorId = movieDto.DirectorId
            };

            _context.Movies.Add(movie);

            await _context.SaveChangesAsync();

            var response = new MovieResponseDto
            {
                Id = movie.Id,
                Title = movie.Title,
                Genre = movie.Genre,
                ReleaseYear = movie.ReleaseYear,
                Rating = movie.Rating
            };

            _logger.LogInformation(
                "Movie with Id {MovieId} added successfully",
                movie.Id);

            return response;
        }

        // PUT: Update Movie
        public async Task<bool> UpdateMovieAsync(
            int id,
            MovieUpdateDto movieDto)
        {
            _logger.LogInformation(
                "Updating movie with Id {MovieId}",
                id);

            var existing = await _context.Movies.FindAsync(id);

            if (existing == null)
            {
                _logger.LogWarning(
                    "Movie with Id {MovieId} was not found",
                    id);

                return false;
            }

            existing.Title = movieDto.Title;
            existing.Genre = movieDto.Genre;
            existing.ReleaseYear = movieDto.ReleaseYear;
            existing.Rating = movieDto.Rating;
            existing.Budget = movieDto.Budget;
            existing.DirectorId = movieDto.DirectorId;

            await _context.SaveChangesAsync();

            _logger.LogInformation(
                "Movie with Id {MovieId} updated successfully",
                id);

            return true;
        }

        // DELETE: Delete Movie
        public async Task<bool> DeleteMovieAsync(int id)
        {
            _logger.LogInformation(
                "Deleting movie with Id {MovieId}",
                id);

            var movie = await _context.Movies.FindAsync(id);

            if (movie == null)
            {
                _logger.LogWarning(
                    "Movie with Id {MovieId} was not found",
                    id);

                return false;
            }

            _context.Movies.Remove(movie);

            await _context.SaveChangesAsync();

            _logger.LogInformation(
                "Movie with Id {MovieId} deleted successfully",
                id);

            return true;
        }
    }
}