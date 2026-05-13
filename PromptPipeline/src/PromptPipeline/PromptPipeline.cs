public class PromptPipeline
{
    private readonly List<IMiddleware> _middlewares = new();

    public IReadOnlyList<IMiddleware> Middlewares => _middlewares;

    public PromptPipeline Use(IMiddleware middleware)
    {
        _middlewares.Add(middleware);
        return this;
    }

    public async Task ExecuteAsync(AgentContext context)
    {
        var index = -1;

        async Task Next()
        {
            index++;

            if (index < _middlewares.Count)
            {
                var current = _middlewares[index];
                await current.InvokeAsync(context, Next);
            }
        }

        await Next();
    }
}