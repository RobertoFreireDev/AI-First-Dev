namespace CLIAirpg.Core;

public class Enemy
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Level { get; set; }
    public int MaxHealth { get; set; }
    public int Health { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }
    public int ExperienceReward { get; set; }
    public int GoldReward { get; set; }
    public List<Item> Loot { get; set; }

    public bool IsAlive => Health > 0;

    public Enemy(string id, string name, string description, int level, int maxHealth, int attack, int defense, int experienceReward, int goldReward, List<Item>? loot = null)
    {
        Id = id;
        Name = name;
        Description = description;
        Level = level;
        MaxHealth = maxHealth;
        Health = MaxHealth;
        Attack = attack;
        Defense = defense;
        ExperienceReward = experienceReward;
        GoldReward = goldReward;
        Loot = loot ?? new List<Item>();
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health < 0)
        {
            Health = 0;
        }
    }
}
