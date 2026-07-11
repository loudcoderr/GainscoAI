namespace GainscoAI.Application.Features.Documents;

public interface IDocumentService
{
    string Upload(UploadDocumentRequest request);
}