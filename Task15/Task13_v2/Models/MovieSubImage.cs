namespace Task13.Models
{
    public class MovieSubImage
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Img { get; set; } = string.Empty;
        public int MovieId { get; set; }
        public Movie Movie { get; set; } = default!;
    }
}
