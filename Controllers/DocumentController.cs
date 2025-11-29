using Microsoft.AspNetCore.Mvc;

namespace WebApplication16.Controllers
{
    public class DocumentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upload(int id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Upload(int id, IFormFile file)
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            return View();
        }
    }
}
