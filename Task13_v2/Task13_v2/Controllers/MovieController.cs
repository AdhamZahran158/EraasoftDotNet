using Microsoft.AspNetCore.Mvc;

namespace Task13.Controllers
{
    public class MovieController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
