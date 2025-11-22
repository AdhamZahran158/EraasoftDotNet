namespace Task13_v2.ViewModels
{
    public record MovieVM
    {
        public string? Name { get; set; }
        public int CatId { get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public int CinemaId { get; set; }
        public double Price { get; set; }
        public bool Status { get; set; }
        public string? MainImg { get; set; }

    }
}
