namespace CLIAirpg.Core;

public class Location
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<string> ConnectedLocationIds { get; set; }

    public Location(string id, string name, string description)
    {
        Id = id;
        Name = name;
        Description = description;
        ConnectedLocationIds = new List<string>();
    }

    public void ConnectTo(string locationId)
    {
        if (!ConnectedLocationIds.Contains(locationId))
        {
            ConnectedLocationIds.Add(locationId);
        }
    }
}
