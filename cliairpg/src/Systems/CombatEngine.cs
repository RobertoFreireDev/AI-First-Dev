using CLIAirpg.Core;
using CLIAirpg.UI;

namespace CLIAirpg.Systems;

public enum CombatOutcome
{
    Victory,
    Fled,
    Defeat
}

public static class CombatEngine
{
    private static readonly Random _random = new();

    public static CombatOutcome EngageCombat(Player player, Enemy enemy)
    {
        ConsoleRenderer.ClearScreen();
        ConsoleRenderer.PrintTitle($"Combat Encounter: {enemy.Name}");
        ConsoleRenderer.PrintMessage(enemy.Description);

        while (player.IsAlive && enemy.IsAlive)
        {
            ConsoleRenderer.PrintSection("Battle Status");
            Console.WriteLine($"Enemy: {enemy.Name} (Level {enemy.Level}) - HP: {enemy.Health}/{enemy.MaxHealth}");
            ConsoleRenderer.PrintPlayerStatus(player);

            ConsoleRenderer.PrintSection("Battle Actions");
            Console.WriteLine("1. Attack");
            Console.WriteLine("2. Use Health Potion");
            Console.WriteLine("3. Attempt to Flee");

            int action = InputHandler.GetMenuChoice(3);

            switch (action)
            {
                case 1:
                    PerformPlayerAttack(player, enemy);
                    break;
                case 2:
                    if (!TryUseHealthPotion(player))
                    {
                        ConsoleRenderer.PrintError("You don't have a usable healing potion.");
                        continue;
                    }
                    break;
                case 3:
                    if (TryFlee(player, enemy))
                    {
                        return CombatOutcome.Fled;
                    }
                    break;
            }

            if (!enemy.IsAlive)
            {
                break;
            }

            PerformEnemyAttack(player, enemy);
            ConsoleRenderer.Pause();
        }

        if (!player.IsAlive)
        {
            ConsoleRenderer.PrintError("You have been defeated in battle.");
            return CombatOutcome.Defeat;
        }

        if (!enemy.IsAlive)
        {
            ConsoleRenderer.PrintSuccess($"You defeated {enemy.Name}!");
            return CombatOutcome.Victory;
        }

        return CombatOutcome.Fled;
    }

    private static void PerformPlayerAttack(Player player, Enemy enemy)
    {
        int damage = CalculateDamage(player.Attack, enemy.Defense);
        enemy.TakeDamage(damage);
        ConsoleRenderer.PrintMessage($"You strike {enemy.Name} for {damage} damage.");
    }

    private static void PerformEnemyAttack(Player player, Enemy enemy)
    {
        int damage = CalculateDamage(enemy.Attack, player.Defense);
        player.TakeDamage(damage);
        ConsoleRenderer.PrintWarning($"{enemy.Name} attacks you for {damage} damage.");
    }

    private static bool TryUseHealthPotion(Player player)
    {
        var potion = player.Inventory.Items.FirstOrDefault(item => item.Id == "health_potion" && item.HealAmount > 0);
        if (potion == null)
        {
            return false;
        }

        player.Heal(potion.HealAmount);
        player.Inventory.RemoveItem(potion.Id);
        ConsoleRenderer.PrintSuccess($"You use a {potion.Name} and recover {potion.HealAmount} HP.");
        return true;
    }

    private static bool TryFlee(Player player, Enemy enemy)
    {
        int fleeChance = 30 + player.Dexterity * 2 - enemy.Level * 8;
        int roll = _random.Next(100);

        if (roll < Math.Max(10, Math.Min(75, fleeChance)))
        {
            ConsoleRenderer.PrintWarning("You manage to escape the battle.");
            return true;
        }

        ConsoleRenderer.PrintWarning("You fail to flee.");
        return false;
    }

    private static int CalculateDamage(int attack, int defense)
    {
        int damage = attack + _random.Next(0, 4) - defense;
        return Math.Max(1, damage);
    }
}
