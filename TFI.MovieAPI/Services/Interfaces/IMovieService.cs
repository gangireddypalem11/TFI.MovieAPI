using TFI.MovieAPI.DTOS;

namespace TFI.MovieAPI.Services.Interfaces
{
    public interface IMovieService
    {
        Task<List<MovieResponseDto>> GetMoviesAsync();

        Task<MovieResponseDto?> GetMovieByIdAsync(int id);

        Task<MovieResponseDto> AddMovieAsync(MovieCreateDto movieDto);

        Task<bool> UpdateMovieAsync(int id, MovieUpdateDto movieDto);

        Task<bool> DeleteMovieAsync(int id);
    }
}