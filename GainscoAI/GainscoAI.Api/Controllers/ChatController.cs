using GainscoAI.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace GainscoAI.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChatController : ControllerBase
{
    private readonly ChatService _chatService;

    public ChatController(ChatService chatService)
    {
        _chatService = chatService;
    }

    [HttpGet("ask")]
    public async Task<IActionResult> Ask(string question)
    {
        var answer = await _chatService.AskAsync(question);

        return Ok(new
        {
            Question = question,
            Answer = answer
        });
    }
}