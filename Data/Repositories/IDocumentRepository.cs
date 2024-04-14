using NeoEditAPI.Models;

namespace NeoEdit.Api.Data.Repositories
{
    public interface IDocumentRepository
    {
        Task<IEnumerable<Document>> GetAllDocumentsAsync();
        Task<Document> GetDocumentByIdAsync(int id);
        Task<Document> CreateDocumentAsync(Document document);
        Task UpdateDocumentAsync(Document document);
        Task DeleteDocumentAsync(int id);
    }
}
