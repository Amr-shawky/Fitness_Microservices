namespace SmartCoachService
{
    // Request Models
    public record GeminiRequest(List<GeminiContent> Contents);
    public record GeminiContent(List<GeminiPart> Parts, string Role = "user");
    public record GeminiPart(string Text);

    // Response Models
    public record GeminiResponse(List<GeminiCandidate> Candidates);
    public record GeminiCandidate(GeminiContent Content, string FinishReason);
}
