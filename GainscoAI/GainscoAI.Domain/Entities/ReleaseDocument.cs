namespace GainscoAI.Domain.Entities;

public class ReleaseDocument
{
    public int Id { get; set; }

    public string FileName { get; set; } = string.Empty;

    public string BlobUrl { get; set; } = string.Empty;

    public string ReleaseVersion { get; set; } = string.Empty;

    public string State { get; set; } = string.Empty;

    public string Product { get; set; } = string.Empty;

    public DateTime UploadedOn { get; set; }
}