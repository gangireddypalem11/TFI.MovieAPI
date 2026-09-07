using System.ComponentModel.DataAnnotations;

namespace TFI.MovieAPI.DTOS
{
    public class MovieCreateDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Genre { get; set; }
        [Range(1900, 2100)]
        public int ReleaseYear { get; set; }
        [Range(0, 10)]
        public decimal Rating { get; set; }
        [Range(0, double.MaxValue)]
        public decimal Budget { get; set; }
        [Range(1, int.MaxValue)]
        public int DirectorId { get; set; }
    }
}
