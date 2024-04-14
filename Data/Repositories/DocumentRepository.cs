using Microsoft.EntityFrameworkCore;
using NeoEditAPI.Models;

namespace NeoEdit.Api.Data.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly DataContext _context;

        public DocumentRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Document>> GetAllDocumentsAsync()
        {
            return await _context.Documents.ToListAsync();
        }

        public async Task<Document> GetDocumentByIdAsync(int id)
        {
            return await _context.Documents.FindAsync(id);
        }

        public async Task<Document> CreateDocumentAsync(Document document)
        {
            _context.Documents.Add(document);
            await _context.SaveChangesAsync();
            return document;
        }

        public async Task UpdateDocumentAsync(Document document)
        {
            _context.Entry(document).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDocumentAsync(int id)
        {
            var document = await GetDocumentByIdAsync(id);
            if (document != null)
            {
                _context.Documents.Remove(document);
                await _context.SaveChangesAsync();
            }
        }
    }
}
