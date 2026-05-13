public class TimingMiddleware : IMiddleware
{
    public string Name => "Timing";

    public async Task InvokeAsync(AgentContext ctx, Func<Task> next)
    {
        var sw = Stopwatch.StartNew();

        await next();

        sw.Stop();
        ctx.Logs.Add($"Took {sw.ElapsedMilliseconds}ms");
    }
}