public class LoggingMiddleware : IMiddleware
{
    public string Name => "Logging";

    public async Task InvokeAsync(AgentContext ctx, Func<Task> next)
    {
        ctx.Logs.Add("Input received");
        await next();
        ctx.Logs.Add("Pipeline finished");
    }
}