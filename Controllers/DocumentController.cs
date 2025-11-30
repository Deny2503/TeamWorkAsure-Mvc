using Microsoft.AspNetCore.Mvc;
using WebApplication16.Data.Models;
using WebApplication16.Repositories;
using WebApplication16.Services;

namespace WebApplication16.Controllers
{
    public class DocumentController : Controller
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly BlobService _blobService;

        public DocumentController(IDocumentRepository documentRepository, BlobService blobService)
        {
            _documentRepository = documentRepository;
            _blobService = blobService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Login", "Auth");

            var documents = await _documentRepository.GetUserDocumentsAsync(userId.Value);
            return View(documents);
        }

        public IActionResult Upload()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Login", "Auth");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(string title, IFormFile file)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Login", "Auth");

            if (file == null || file.Length == 0)
            {
                ModelState.AddModelError("", "Choose file for upload");
                return View();
            }

            var blobFileName = file.FileName;

            await _blobService.UploadAsync(file, blobFileName);

            var doc = new Document
            {
                Title = title,
                BlobName = blobFileName,
                UserId = userId.Value,
                Status = Document.DocumentStatus.Draft
            };

            await _documentRepository.AddAsync(doc);
            await _documentRepository.SaveChangesAsync();

            doc.Status = Document.DocumentStatus.InReview;
            await _documentRepository.UpdateAsync(doc);
            await _documentRepository.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Login", "Auth");

            var document = await _documentRepository.GetAsync(id);

            if (document == null || document.UserId != userId.Value)
                return NotFound();

            return View(document);
        }
    }
}
