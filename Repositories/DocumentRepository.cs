using Microsoft.EntityFrameworkCore;
using WebApplication16.Data;
using WebApplication16.Data.Models;

namespace WebApplication16.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly AppDbContext _context;

        public DocumentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Document?> GetAsync(int id)
        {
            return await _context.Documents
                .Include(d => d.User)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<IEnumerable<Document>> GetUserDocumentsAsync(int userId)
        {
            return await _context.Documents
                .Where(d => d.UserId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Document>> GetAllAsync()
        {
            return await _context.Documents
                .Include(d => d.User)
                .ToListAsync();
        }

        public async Task<IEnumerable<Document>> GetByStatusAsync(Document.DocumentStatus status)
        {
            return await _context.Documents
                .Where(d => d.Status == status)
                .ToListAsync();
        }

        public async Task AddAsync(Document document)
        {
            await _context.Documents.AddAsync(document);
        }

        public async Task UpdateAsync(Document document)
        {
            _context.Documents.Update(document);
        }

        public async Task DeleteAsync(int id)
        {
            var doc = await GetAsync(id);
            if (doc != null)
                _context.Documents.Remove(doc);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
