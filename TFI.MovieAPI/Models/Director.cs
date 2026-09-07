namespace TFI.MovieAPI.Models
{
    public class Director
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Industry { get; set; }

        public List<Movie> Movies { get; set; } = new List<Movie>();
    }
}