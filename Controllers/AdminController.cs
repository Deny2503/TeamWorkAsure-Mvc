using Microsoft.AspNetCore.Mvc;

namespace WebApplication16.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Documents()
        {
            return View();
        }

        public IActionResult Document(int id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult ChangeStatus(int id, DocumentStatus newStatus)
        {
            return View();
        }

        public IActionResult History(int id)
        {
            return View();
        }

        public IActionResult Export()
        {
            return View();
        }
    }
}
