using Microsoft.AspNetCore.Mvc;
using NeoEditAPI.Models;

[Route("api/[controller]")]
[ApiController]
public class DocumentsController : ControllerBase
{
	private static List<Document> documents = new List<Document>
	{
		new Document { Id = 1, Title = "Example", Content = "This is an example document content." }
	};

	[HttpGet]
	public ActionResult<List<Document>> GetAll()
	{
		return documents;
	}

	[HttpGet("{id}")]
	public ActionResult<Document> Get(int id)
	{
		var document = documents.FirstOrDefault(d => d.Id == id);
		if (document == null)
			return NotFound();
		return document;
	}

	[HttpPost]
	public ActionResult<Document> Post(Document document)
	{
		document.Id = documents.Max(d => d.Id) + 1;
		documents.Add(document);
		return CreatedAtAction(nameof(Get), new { id = document.Id }, document);
	}

	[HttpPut("{id}")]
	public IActionResult Put(int id, Document document)
	{
		var existingDocument = documents.FirstOrDefault(d => d.Id == id);
		if (existingDocument == null)
			return NotFound();

		existingDocument.Title = document.Title;
		existingDocument.Content = document.Content;

		return NoContent();
	}

	[HttpDelete("{id}")]
	public IActionResult Delete(int id)
	{
		var document = documents.FirstOrDefault(d => d.Id == id);
		if (document == null)
			return NotFound();

		documents.Remove(document);
		return NoContent();
	}
}
