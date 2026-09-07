namespace TFI.MovieAPI.DTOS
{
    public class MovieResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public int ReleaseYear { get; set; }
        public decimal Rating { get; set; }

    }
}
