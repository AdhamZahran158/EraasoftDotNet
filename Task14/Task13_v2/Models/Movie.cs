namespace Task13.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public string MainImg { get; set; }
        public double Price { get; set; }
        public bool Status { get; set; }
        public int CategoryId { get; set; }
        public int CinemaId { get; set; }
        public List<Actor> Actors { get; set; } = default!;
        public List<MovieSubImage> MovieSubImages { get; set; } = default!;
        public Category Category { get; set; } = default!;
        public Cinema Cinema { get; set; } = default!;

    }
}
