namespace CLIAirpg.Core;

public class Item
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Weight { get; set; } // in units

    public Item(string id, string name, string description, int weight)
    {
        Id = id;
        Name = name;
        Description = description;
        Weight = weight;
    }
}
