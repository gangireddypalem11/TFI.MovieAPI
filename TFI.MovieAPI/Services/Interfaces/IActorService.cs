using TFI.MovieAPI.DTOS;

namespace TFI.MovieAPI.Services.Interfaces
{
    public interface IActorService
    {
        Task<ActorResponseDto> AddActorAsync(ActorCreateDto actorDto);

        Task<List<ActorResponseDto>> GetActorsAsync();

        Task<ActorResponseDto?> GetActorByIdAsync(int id);
    }
}
