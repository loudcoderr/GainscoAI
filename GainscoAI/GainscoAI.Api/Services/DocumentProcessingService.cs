using GainscoAI.Application.AI;
using GainscoAI.Application.AI.Models;

using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;

namespace GainscoAI.Api.Services;

public class DocumentProcessingService
{
    private readonly EmbeddingService _embeddingService;
    private readonly SearchService _searchService;

    public DocumentProcessingService(
        EmbeddingService embeddingService,
        SearchService searchService)
    {
        _embeddingService = embeddingService;
        _searchService = searchService;
    }

    public async Task<List<DocumentChunk>> ProcessPdfAsync(
        Stream pdfStream,
        string fileName)
    {
        using var pdfReader = new PdfReader(pdfStream);
        using var pdfDocument = new PdfDocument(pdfReader);

        var text = "";

        for (int i = 1; i <= pdfDocument.GetNumberOfPages(); i++)
        {
            text += PdfTextExtractor.GetTextFromPage(pdfDocument.GetPage(i));
        }

        var chunker = new TextChunker();
        var chunks = chunker.Chunk(text);

        var result = new List<DocumentChunk>();

        int chunkNumber = 1;

        foreach (var chunk in chunks)
        {
            var embedding = await _embeddingService.GenerateEmbeddingAsync(chunk);

            result.Add(new DocumentChunk
            {
                FileName = fileName,
                ChunkNumber = chunkNumber++,
                Content = chunk,
                Embedding = embedding
            });
        }
        Console.WriteLine($"Uploading {result.Count} chunks...");
        await _searchService.UploadChunksAsync(
    result.Select(x => new
    {
        id = x.Id,
        fileName = x.FileName,
        chunkNumber = x.ChunkNumber,
        content = x.Content,
        contentVector = x.Embedding
    }));

        return result;
    }
}