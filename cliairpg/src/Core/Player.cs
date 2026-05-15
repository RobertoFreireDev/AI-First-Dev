namespace CLIAirpg.Core;

public enum CharacterClass
{
    Warrior,
    Mage,
    Rogue,
    Paladin
}

public class Player
{
    public string Name { get; set; }
    public CharacterClass Class { get; set; }
    public int Level { get; set; }
    public int Experience { get; set; }
    public int Health { get; set; }
    public int MaxHealth { get; set; }
    public int Mana { get; set; }
    public int MaxMana { get; set; }
    public int Strength { get; set; }
    public int Intelligence { get; set; }
    public int Dexterity { get; set; }
    public int Gold { get; set; }
    public Inventory Inventory { get; set; }

    public int Attack => Strength + Level * 2;
    public int Defense => Dexterity + Level;
    public int MagicPower => Intelligence + Level;
    public int ExperienceToNextLevel => Level * 100;

    public Player(string name, CharacterClass characterClass)
    {
        Name = name;
        Class = characterClass;
        Level = 1;
        Experience = 0;
        MaxHealth = 100;
        Health = MaxHealth;
        MaxMana = 50;
        Mana = MaxMana;
        Strength = 10;
        Intelligence = 10;
        Dexterity = 10;
        Gold = 0;
        Inventory = new Inventory(maxWeight: 100);
    }

    public bool TakeDamage(int damage)
    {
        Health -= damage;
        if (Health < 0) Health = 0;
        return Health > 0;
    }

    public void Heal(int amount)
    {
        Health = Math.Min(Health + amount, MaxHealth);
    }

    public void RestoreMana(int amount)
    {
        Mana = Math.Min(Mana + amount, MaxMana);
    }

    public void GainExperience(int amount)
    {
        Experience += amount;
        while (Experience >= ExperienceToNextLevel)
        {
            Experience -= ExperienceToNextLevel;
            LevelUp();
        }
    }

    private void LevelUp()
    {
        Level++;
        MaxHealth += 20;
        Health = MaxHealth;
        MaxMana += 10;
        Mana = MaxMana;
        Strength += 2;
        Intelligence += 2;
        Dexterity += 2;
    }

    public void AddGold(int amount)
    {
        Gold += amount;
    }

    public bool SpendGold(int amount)
    {
        if (Gold >= amount)
        {
            Gold -= amount;
            return true;
        }
        return false;
    }

    public bool IsAlive => Health > 0;
}
