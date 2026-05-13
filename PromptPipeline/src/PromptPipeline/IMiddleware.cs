public interface IMiddleware
{
    string Name { get; }

    Task InvokeAsync(AgentContext context, Func<Task> next);
}