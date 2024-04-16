using NeoEdit.Api.Data.Repositories;
using NeoEdit.Api.Eventing;
using NeoEditAPI.Models;

namespace NeoEdit.Api.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IDocumentRepository _repository;
        private readonly RabbitMQClient _rabbitMQClient;

        public DocumentService(IDocumentRepository repository, RabbitMQClient rabbitMQClient)
        {
            _repository = repository;
            _rabbitMQClient = rabbitMQClient;
        }

        public async Task<IEnumerable<Document>> GetAllDocumentsAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Document> GetDocumentByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Document> CreateDocumentAsync(Document document)
        {
            return await _repository.AddAsync(document);
        }

        public async Task UpdateDocumentAsync(Document document)
        {
            await _repository.UpdateAsync(document);
        }

        public async Task DeleteDocumentAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
