using Microsoft.EntityFrameworkCore;
using TFI.MovieAPI.Models;

namespace TFI.MovieAPI.Data
{
    public class TFIDbContext : DbContext
    {
        public TFIDbContext(DbContextOptions<TFIDbContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<Director> Directors { get; set; }
        public DbSet<Actor> Actors { get; set; }
    }
}