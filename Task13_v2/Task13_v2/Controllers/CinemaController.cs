using Microsoft.AspNetCore.Mvc;

namespace Task13.Controllers
{
    public class CinemaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
