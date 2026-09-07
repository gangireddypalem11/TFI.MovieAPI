using Microsoft.AspNetCore.Mvc;
using TFI.MovieAPI.DTOS;
using TFI.MovieAPI.Services.Interfaces;

namespace TFI.MovieAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;
        private readonly ILogger<MoviesController> _logger;

        public MoviesController(
            ILogger<MoviesController> logger,
            IMovieService movieService)
        {
            _logger = logger;
            _movieService = movieService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMovies()
        {
            _logger.LogInformation("GetMovies API called");

            var movies = await _movieService.GetMoviesAsync();

            _logger.LogInformation(
                "Retrieved {Count} movies",
                movies.Count);

            return Ok(movies);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovieById(int id)
        {
            _logger.LogInformation(
                "Getting movie with Id {MovieId}",
                id);

            var movie = await _movieService.GetMovieByIdAsync(id);

            if (movie == null)
            {
                _logger.LogWarning(
                    "Movie with Id {MovieId} was not found",
                    id);

                return NotFound("Movie not found");
            }

            return Ok(movie);
        }

        [HttpPost]
        public async Task<IActionResult> AddMovie(MovieCreateDto movieDto)
        {
            var movie = await _movieService.AddMovieAsync(movieDto);

            return CreatedAtAction(
                nameof(GetMovieById),
                new { id = movie.Id },
                movie);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovieById(
            int id,
            MovieUpdateDto movieDto)
        {
            var updated = await _movieService.UpdateMovieAsync(id, movieDto);

            if (!updated)
            {
                return NotFound("Movie not found");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovieByID(int id)
        {
            var deleted = await _movieService.DeleteMovieAsync(id);

            if (!deleted)
            {
                return NotFound("Movie not found");
            }

            return NoContent();
        }
    }
}