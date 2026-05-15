namespace CLIAirpg.Core;

public class Item
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Weight { get; set; } // in units
    public int Value { get; set; }
    public int HealAmount { get; set; }
    public string Type { get; set; }

    public Item(string id, string name, string description, int weight, int value = 0, int healAmount = 0, string type = "Misc")
    {
        Id = id;
        Name = name;
        Description = description;
        Weight = weight;
        Value = value;
        HealAmount = healAmount;
        Type = type;
    }
}
