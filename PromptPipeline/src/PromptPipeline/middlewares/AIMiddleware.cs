public interface IAIService
{
    Task<string> GenerateAsync(string input);
}

public class FakeAIService : IAIService
{
    public async Task<string> GenerateAsync(string input)
    {
        await Task.Delay(1000);
        return $"AI Response: You said -> {input}";
    }
}

public class AIMiddleware : IMiddleware
{
    private readonly IAIService _ai;

    public string Name => "AI";

    public AIMiddleware(IAIService ai)
    {
        _ai = ai;
    }

    public async Task InvokeAsync(AgentContext ctx, Func<Task> next)
    {
        AnsiConsole.Status()
                    .Start("Thinking...", _ =>
                    {
                        Thread.Sleep(1000);
                    });
        ctx.Output = await _ai.GenerateAsync(ctx.Input);
        await next();
    }
}