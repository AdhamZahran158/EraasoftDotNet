namespace Task13_v2.ViewModels
{
    public class MovieListVM
    {
        public int MovieId { get; set; }
        public string MovieName { get; set; }
        public string CategoryName { get; set; }
        public double? MoviePrice { get; set; }
        public bool MovieStatus { get; set; }
        public DateTime MovieDate { get; set; }
        public string MovieDescription { get; set; }
        public string MovieMainImg { get; set; }
    }
}
