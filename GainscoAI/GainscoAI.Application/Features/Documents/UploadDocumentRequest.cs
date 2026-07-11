namespace GainscoAI.Application.Features.Documents;

public class UploadDocumentRequest
{
    public string FileName { get; set; } = string.Empty;

    public string ReleaseVersion { get; set; } = string.Empty;

    public string State { get; set; } = string.Empty;

    public string Product { get; set; } = string.Empty;
}