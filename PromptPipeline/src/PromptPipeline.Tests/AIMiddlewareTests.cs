namespace PromptPipeline.Tests;

public class AIMiddlewareTests
{
    [Fact]
    public async Task AIMiddleware_Should_Set_Output()
    {
        var ctx = new AgentContext { Input = "hello" };
        var middleware = new AIMiddleware(new FakeAIService());

        await middleware.InvokeAsync(ctx, () => Task.CompletedTask);

        Assert.Contains("hello", ctx.Output);
    }
}
