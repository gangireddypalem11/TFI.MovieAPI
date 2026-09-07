namespace TFI.MovieAPI.Models
{
    public class Movie
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int ReleaseYear { get; set; }

        public string Genre { get; set; }

        public decimal Budget { get; set; }

        public decimal Rating { get; set; }

        public int DirectorId { get; set; }

        public Director? Director { get; set; }
    }
}