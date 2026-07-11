using Azure;
using Azure.AI.OpenAI;
using OpenAI.Embeddings;

namespace GainscoAI.Api.Services;

public class EmbeddingService
{
    private readonly EmbeddingClient _embeddingClient;

    public EmbeddingService(IConfiguration configuration)
    {
        var endpoint = new Uri(configuration["AzureOpenAI:Endpoint"]!);
        var apiKey = configuration["AzureOpenAI:ApiKey"]!;

        var client = new AzureOpenAIClient(
            endpoint,
            new AzureKeyCredential(apiKey));

        _embeddingClient = client.GetEmbeddingClient(
            configuration["AzureOpenAI:EmbeddingDeployment"]!);
    }

    public async Task<float[]> GenerateEmbeddingAsync(string text)
    {
        var result = await _embeddingClient.GenerateEmbeddingAsync(text);

        return result.Value.ToFloats().ToArray();
    }
}