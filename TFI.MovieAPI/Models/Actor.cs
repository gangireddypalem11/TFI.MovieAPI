using System.ComponentModel.DataAnnotations;

namespace TFI.MovieAPI.Models
{
    public class Actor
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Industry { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}