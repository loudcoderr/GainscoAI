using Azure;
using Azure.AI.OpenAI;
using OpenAI.Chat;

namespace GainscoAI.Api.Services;

public class ChatCompletionService
{
    private readonly ChatClient _chatClient;

    public ChatCompletionService(IConfiguration configuration)
    {
        var endpoint = new Uri(configuration["AzureOpenAI:Endpoint"]!);
        var apiKey = configuration["AzureOpenAI:ApiKey"]!;

        var client = new AzureOpenAIClient(
            endpoint,
            new AzureKeyCredential(apiKey));

        _chatClient = client.GetChatClient(
            configuration["AzureOpenAI:ChatDeployment"]!);
    }

    public async Task<string> AskQuestionAsync(
        string question,
        List<string> context)
    {
        var prompt =
$"""
Answer the question only from the provided context.

Context:

{string.Join("\n\n", context)}

Question:
{question}
""";

        var messages = new List<ChatMessage>
        {
            new SystemChatMessage("You are a helpful assistant."),
            new UserChatMessage(prompt)
        };

        var response = await _chatClient.CompleteChatAsync(messages);

        return response.Value.Content[0].Text;
    }
}