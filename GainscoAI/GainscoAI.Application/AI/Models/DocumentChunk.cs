namespace GainscoAI.Application.AI.Models;

public class DocumentChunk
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string FileName { get; set; } = string.Empty;

    public int ChunkNumber { get; set; }

    public string Content { get; set; } = string.Empty;

    public float[] Embedding { get; set; } = Array.Empty<float>();
}