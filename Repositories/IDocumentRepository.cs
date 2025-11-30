using WebApplication16.Data.Models;

namespace WebApplication16.Repositories
{
    public interface IDocumentRepository
    {
        Task<Document?> GetAsync(int id);
        Task<IEnumerable<Document>> GetUserDocumentsAsync(int userId);
        Task<IEnumerable<Document>> GetAllAsync();
        Task<IEnumerable<Document>> GetByStatusAsync(Document.DocumentStatus status);

        Task AddAsync(Document document);
        Task UpdateAsync(Document document);
        Task DeleteAsync(int id);

        Task SaveChangesAsync();
    }
}
