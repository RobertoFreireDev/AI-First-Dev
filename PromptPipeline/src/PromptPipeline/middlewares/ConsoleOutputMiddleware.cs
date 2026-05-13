public class ConsoleOutputMiddleware : IMiddleware
{
    public string Name => "ConsoleOutput";

    public async Task InvokeAsync(AgentContext ctx, Func<Task> next)
    {
        await next();

        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine($"[green]{ctx.Output}[/]");
    }
}