using GainscoAI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using GainscoAI.Application.Features.Documents;
using GainscoAI.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<IDocumentService, DocumentService>();
builder.Services.AddScoped<EmbeddingService>();
builder.Services.AddScoped<DocumentProcessingService>();
builder.Services.AddScoped<SearchService>();
builder.Services.AddScoped<ChatService>();
builder.Services.AddScoped<ChatCompletionService>();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=gainscoai.db"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("Angular", policy =>
    {
        policy
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
var app = builder.Build();
app.UseCors("Angular");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.MapGet("/embedding-test", async (EmbeddingService embeddingService) =>
{
    var vector = await embeddingService.GenerateEmbeddingAsync(
        "Dependency Injection is a design pattern.");

    return Results.Ok(new
    {
        Dimensions = vector.Length,
        First10Values = vector.Take(10)
    });
});

app.Run();