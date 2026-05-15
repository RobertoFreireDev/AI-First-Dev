using CLIAirpg.Core;

namespace CLIAirpg.Systems;

public static class EncounterService
{
    private static readonly Random _random = new();

    public static Enemy? CreateEncounter(string locationId)
    {
        int roll = _random.Next(100);

        return locationId switch
        {
            "forest" => roll < 70 ? CreateForestSpider() : null,
            "cave" => roll < 80 ? CreateCaveBat() : null,
            "starting_area" => roll < 25 ? CreateBandit() : null,
            _ => null,
        };
    }

    private static Enemy CreateForestSpider()
    {
        return new Enemy(
            id: "forest_spider",
            name: "Giant Forest Spider",
            description: "A venomous spider drops from the branches, its eyes glinting in the twilight.",
            level: 2,
            maxHealth: 50,
            attack: 8,
            defense: 3,
            experienceReward: 40,
            goldReward: 20,
            loot: new List<Item>
            {
                new Item("health_potion", "Health Potion", "Restores 30 HP", 2, 0, 30, "Consumable"),
                new Item("spider_silk", "Spider Silk", "A sticky strand of spider silk.", 1, 5, 0, "Material")
            }
        );
    }

    private static Enemy CreateCaveBat()
    {
        return new Enemy(
            id: "cave_bat",
            name: "Cave Bat",
            description: "A screeching bat swoops from the cave ceiling, claws outstretched.",
            level: 3,
            maxHealth: 60,
            attack: 10,
            defense: 4,
            experienceReward: 50,
            goldReward: 25,
            loot: new List<Item>
            {
                new Item("health_potion", "Health Potion", "Restores 30 HP", 2, 0, 30, "Consumable"),
                new Item("bat_wing", "Bat Wing", "A leathery wing used for potion ingredients.", 1, 8, 0, "Material")
            }
        );
    }

    private static Enemy CreateBandit()
    {
        return new Enemy(
            id: "starting_bandit",
            name: "Rogue Bandit",
            description: "A sneaky bandit lurks in the alley looking for an easy mark.",
            level: 1,
            maxHealth: 40,
            attack: 6,
            defense: 2,
            experienceReward: 25,
            goldReward: 15,
            loot: new List<Item>
            {
                new Item("health_potion", "Health Potion", "Restores 30 HP", 2, 0, 30, "Consumable"),
                new Item("rusty_dagger", "Rusty Dagger", "A battered dagger with a keen edge.", 4, 8, 0, "Weapon")
            }
        );
    }
}
