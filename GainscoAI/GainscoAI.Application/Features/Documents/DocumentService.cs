namespace GainscoAI.Application.Features.Documents;

public class DocumentService : IDocumentService
{
    public string Upload(UploadDocumentRequest request)
    {
        return $"Document '{request.FileName}' uploaded successfully.";
    }
}