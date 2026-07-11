using Azure;
using Azure.Search.Documents;

namespace GainscoAI.Api.Services;

public class SearchService
{
    private readonly SearchClient _searchClient;
    public SearchClient Client => _searchClient;
    public SearchService(IConfiguration configuration)
    {
        var endpoint = new Uri(configuration["AzureSearch:Endpoint"]!);
        var apiKey = configuration["AzureSearch:ApiKey"]!;

        _searchClient = new SearchClient(
            endpoint,
            "documents",
            new AzureKeyCredential(apiKey));
    }

    public async Task UploadChunksAsync(IEnumerable<object> documents)
    {
        await _searchClient.UploadDocumentsAsync(documents);
    }
}