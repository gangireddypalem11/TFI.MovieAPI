using Microsoft.AspNetCore.Mvc;
using TFI.MovieAPI.DTOS;
using TFI.MovieAPI.Services.Interfaces;

namespace TFI.MovieAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActorsController : ControllerBase
    {
        private readonly IActorService _actorService;

        public ActorsController(IActorService actorService)
        {
            _actorService = actorService;
        }

        [HttpPost]
        public async Task<IActionResult> AddActor(ActorCreateDto actorDto)
        {
            var result = await _actorService.AddActorAsync(actorDto);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetActors()
        {
            var result = await _actorService.GetActorsAsync();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetActorById(int id)
        {
            var result = await _actorService.GetActorByIdAsync(id);

            if (result == null)
            {
                return NotFound("Actor not found");
            }

            return CreatedAtAction(
      nameof(GetActorById),
      new { id = result.Id },
      result);
        }
    }
}