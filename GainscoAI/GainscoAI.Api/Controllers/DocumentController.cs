using GainscoAI.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace GainscoAI.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DocumentController : ControllerBase
{
    private readonly DocumentProcessingService _documentProcessingService;

    public DocumentController(DocumentProcessingService documentProcessingService)
    {
        _documentProcessingService = documentProcessingService;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("Please upload a PDF.");

        var documentChunks = await _documentProcessingService.ProcessPdfAsync(
            file.OpenReadStream(),
            file.FileName);

        return Ok(new
        {
            TotalChunks = documentChunks.Count,
            Chunks = documentChunks.Select(c => new
            {
                c.ChunkNumber,
                ChunkPreview = c.Content.Length > 100
                    ? c.Content[..100]
                    : c.Content,
                EmbeddingDimensions = c.Embedding.Length
            })
        });
    }
}