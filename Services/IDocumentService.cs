using NeoEditAPI.Models;

namespace NeoEdit.Api.Services
{
    public interface IDocumentService
    {
        Task<IEnumerable<Document>> GetAllDocumentsAsync();
        Task<Document> GetDocumentByIdAsync(int id);
        Task<Document> CreateDocumentAsync(Document document);
        Task UpdateDocumentAsync(Document document);
        Task DeleteDocumentAsync(int id);
    }
}
