namespace CLIAirpg.Core;

public class GameState
{
    public Player? Player { get; set; }
    public int CurrentTurn { get; set; }
    public Location? CurrentLocation { get; set; }
    public Dictionary<string, Location> Locations { get; set; }
    public List<Quest> ActiveQuests { get; set; }
    public Dictionary<string, object> EventFlags { get; set; }
    public bool IsGameRunning { get; set; }

    public GameState()
    {
        Player = null;
        CurrentTurn = 0;
        CurrentLocation = null;
        Locations = new Dictionary<string, Location>();
        ActiveQuests = new List<Quest>();
        EventFlags = new Dictionary<string, object>();
        IsGameRunning = false;
    }

    public void IncrementTurn()
    {
        CurrentTurn++;
    }

    public void AddLocation(Location location)
    {
        Locations[location.Id] = location;
    }

    public Location? GetLocation(string locationId)
    {
        return Locations.TryGetValue(locationId, out var location) ? location : null;
    }

    public void AddQuest(Quest quest)
    {
        if (!ActiveQuests.Any(q => q.Id == quest.Id))
        {
            ActiveQuests.Add(quest);
        }
    }

    public Quest? FindQuest(string questId)
    {
        return ActiveQuests.FirstOrDefault(q => q.Id == questId);
    }

    public void SetEventFlag(string flagKey, object value)
    {
        EventFlags[flagKey] = value;
    }

    public bool GetEventFlag(string flagKey)
    {
        return EventFlags.TryGetValue(flagKey, out var value) && (bool)value;
    }
}
