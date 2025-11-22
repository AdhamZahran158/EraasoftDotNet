using Task13.Models;

namespace Task13_v2.ViewModels
{
    public class EditMovieVM
    {
        public Movie Movie { get; set; }
        public List<Category> Categories { get; set; }
        public List<Cinema> Cinema { get; set; }
    }
}
