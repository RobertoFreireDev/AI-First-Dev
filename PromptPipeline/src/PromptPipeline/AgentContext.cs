public class AgentContext
{
    public string Input { get; set; } = "";
    public string Output { get; set; } = "";

    public List<string> Logs { get; } = new();
    public Dictionary<string, object> Items { get; } = new();
}