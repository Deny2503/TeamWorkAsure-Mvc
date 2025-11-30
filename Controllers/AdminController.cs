using Microsoft.AspNetCore.Mvc;
using WebApplication16.Data.Models;
using WebApplication16.Models;
using WebApplication16.Repositories;

namespace WebApplication16.Controllers
{
    public class AdminController : Controller
    {
        private readonly IDocumentRepository _documentRepository;
        public AdminController(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }

        public async Task<IActionResult> Index()
        {
            var documents = _documentRepository.GetAllAsync();
            return View(documents);
        }

        public async Task<IActionResult> ReviewDocuments()
        {
            var documents = await _documentRepository.GetByStatusAsync(Document.DocumentStatus.InReview);
            return View(documents);
        }

        [HttpPost]
        public async Task<IActionResult> ApproveDocument(int id)
        {
            var document = await _documentRepository.GetAsync(id);
            if (document == null)
            {
                return NotFound();
            }
            document.Status = Document.DocumentStatus.Approved;
            await _documentRepository.UpdateAsync(document);
            return RedirectToAction("ReviewDocuments");
        }

        [HttpPost]
        public async Task<IActionResult> RejectDocument(int id)
        {
            var document = await _documentRepository.GetAsync(id);
            if (document == null)
            {
                return NotFound();
            }
            document.Status = Document.DocumentStatus.Rejected;
            await _documentRepository.UpdateAsync(document);
            return RedirectToAction("ReviewDocuments");
        }

        public async Task<IActionResult> Details(int id)
        {
            var document = await _documentRepository.GetAsync(id);
            if (document == null)
            {
                return NotFound();
            }
            return View(document);
        }
    }
}
