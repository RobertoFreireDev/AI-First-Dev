namespace CLIAirpg.Core;

public class Quest
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsCompleted { get; set; }
    public int RewardExperience { get; set; }
    public int RewardGold { get; set; }

    public Quest(string id, string title, string description, int rewardExp, int rewardGold)
    {
        Id = id;
        Title = title;
        Description = description;
        IsCompleted = false;
        RewardExperience = rewardExp;
        RewardGold = rewardGold;
    }

    public void Complete()
    {
        IsCompleted = true;
    }
}
