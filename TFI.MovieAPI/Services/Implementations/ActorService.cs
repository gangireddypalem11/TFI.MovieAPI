using System.Linq;
using TFI.MovieAPI.Data;
using TFI.MovieAPI.DTOS;
using TFI.MovieAPI.Services.Interfaces;
using TFI.MovieAPI.Models;
using Microsoft.EntityFrameworkCore;
namespace TFI.MovieAPI.Services.Implementations
{
    public class ActorService: IActorService
    {
        private readonly TFIDbContext _context;
        private readonly ILogger<ActorService> _logger;
        public ActorService(
            TFIDbContext context,
            ILogger<ActorService> logger)
            {
            _context = context;
            _logger = logger;
        }

        public async Task<ActorResponseDto> AddActorAsync(ActorCreateDto actorDto)
        {
            _logger.LogInformation("Adding new actor: {Name}", actorDto.Name);
            var actor = new Actor
            {
                Name = actorDto.Name,
                Industry = actorDto.Industry,
                DateOfBirth = actorDto.DateOfBirth
            };

            _context.Actors.Add(actor);
            await _context.SaveChangesAsync();

            var response = new ActorResponseDto
            {
                Id = actor.Id,
                Name = actor.Name,
                Industry = actor.Industry,
                DateOfBirth = actor.DateOfBirth
            };

            _logger.LogInformation("Added actor with id {Id}", response.Id);
            return response;
        }

        public async Task<List<ActorResponseDto>> GetActorsAsync()
        {
            _logger.LogInformation("Fetching all actors");
            var actors = await _context.Actors.ToListAsync();

            var response = actors.Select(actor => new ActorResponseDto
            {
                Id = actor.Id,
                Name = actor.Name,
                Industry= actor.Industry,
                DateOfBirth = actor.DateOfBirth

            }).ToList();
            _logger.LogInformation("Fetched {Count} actors", response.Count);
            return response;
        }

        public async Task<ActorResponseDto?> GetActorByIdAsync(int id)
        {
            _logger.LogInformation("Fetching actor by id {Id}", id);
            var actor = await _context.Actors.FindAsync(id);
            if (actor == null)
            {
                _logger.LogWarning("Actor with id {Id} not found", id);
                return null;
            }

            return new ActorResponseDto
            {
                Id = actor.Id,
                Name = actor.Name,
                Industry = actor.Industry,
                DateOfBirth = actor.DateOfBirth
            };
        }

    }
}
