using NeoEditAPI.Models;

namespace NeoEdit.Api.Data.Repositories
{
    public interface IDocumentRepository
    {
        Task<IEnumerable<Document>> GetAllAsync();
        Task<Document> GetByIdAsync(int id);
        Task<Document> AddAsync(Document document);
        Task UpdateAsync(Document document);
        Task DeleteAsync(int id);
    }
}
