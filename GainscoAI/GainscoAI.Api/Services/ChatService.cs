using Azure.Search.Documents;
using Azure.Search.Documents.Models;
using System.Text.Json;

namespace GainscoAI.Api.Services;

public class ChatService
{
    private readonly SearchService _searchService;
    private readonly EmbeddingService _embeddingService;
    private readonly ChatCompletionService _chatCompletionService;

    public ChatService(
        SearchService searchService,
        EmbeddingService embeddingService,
        ChatCompletionService chatCompletionService)
    {
        _searchService = searchService;
        _embeddingService = embeddingService;
        _chatCompletionService = chatCompletionService;
    }

    public async Task<string> AskAsync(string question)
    {
        var embedding = await _embeddingService.GenerateEmbeddingAsync(question);

        var vectorQuery = new VectorizedQuery(embedding)
        {
            KNearestNeighborsCount = 5
        };

        vectorQuery.Fields.Add("contentVector");

        var options = new SearchOptions
        {
            Size = 5
        };

        options.VectorSearch = new VectorSearchOptions();
        options.VectorSearch.Queries.Add(vectorQuery);

        var response = await _searchService.Client.SearchAsync<dynamic>(
            searchText: null,
            options);

        var chunks = new List<string>();

        foreach (var result in response.Value.GetResults())
        {
            var json = (JsonElement)result.Document;
            chunks.Add(json.GetProperty("content").GetString()!);
        }

        return await _chatCompletionService.AskQuestionAsync(question, chunks);
    }
}