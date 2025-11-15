using Microsoft.AspNetCore.Mvc;

namespace Task13.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
