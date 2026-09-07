using Microsoft.AspNetCore.Mvc;
using TFI.MovieAPI.Data;
using TFI.MovieAPI.Models;

namespace TFI.MovieAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DirectorsController : ControllerBase
    {
        private readonly TFIDbContext _context;

        public DirectorsController(TFIDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> AddDirector(Director director)
        {
            _context.Directors.Add(director);

            await _context.SaveChangesAsync();

            return Ok(director);
        }
    }
}