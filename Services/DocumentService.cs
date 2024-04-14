using NeoEdit.Api.Data.Repositories;
using NeoEditAPI.Models;

namespace NeoEdit.Api.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IDocumentRepository _documentRepository;

        public DocumentService(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }

        public async Task<IEnumerable<Document>> GetAllDocumentsAsync()
        {
            return await _documentRepository.GetAllDocumentsAsync();
        }

        public async Task<Document> GetDocumentByIdAsync(int id)
        {
            return await _documentRepository.GetDocumentByIdAsync(id);
        }

        public async Task<Document> CreateDocumentAsync(Document document)
        {
            return await _documentRepository.CreateDocumentAsync(document);
        }

        public async Task UpdateDocumentAsync(Document document)
        {
            await _documentRepository.UpdateDocumentAsync(document);
        }

        public async Task DeleteDocumentAsync(int id)
        {
            await _documentRepository.DeleteDocumentAsync(id);
        }
    }
}
