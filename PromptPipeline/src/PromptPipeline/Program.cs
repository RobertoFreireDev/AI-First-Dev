class Program
{
    static async Task Main()
    {
        var pipeline = new PromptPipeline()
            .UseDefault()
            .Use(new AIMiddleware(new FakeAIService()))
            .Use(new ConsoleOutputMiddleware());

        while (true)
        {
            var input = AnsiConsole.Ask<string>("[blue]>[/] Enter prompt (or 'exit'):");

            if (input.ToLower() == "exit")
                break;

            var context = new AgentContext { Input = input };

            await pipeline.ExecuteAsync(context);

            // 🔍 Transparency
            AnsiConsole.Write(new Rule("[yellow]Logs[/]"));
            foreach (var log in context.Logs)
            {
                AnsiConsole.MarkupLine($"[grey]- {log}[/]");
            }

            AnsiConsole.WriteLine();
        }
    }
}