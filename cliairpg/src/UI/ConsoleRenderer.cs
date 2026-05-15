namespace CLIAirpg.UI;

public static class ConsoleRenderer
{
    public static void ClearScreen()
    {
        Console.Clear();
    }

    public static void PrintTitle(string title)
    {
        Console.WriteLine(new string('=', 60));
        Console.WriteLine(title.PadLeft(30 + title.Length / 2));
        Console.WriteLine(new string('=', 60));
    }

    public static void PrintSection(string sectionName)
    {
        Console.WriteLine();
        Console.WriteLine(new string('-', 60));
        Console.WriteLine($">> {sectionName}");
        Console.WriteLine(new string('-', 60));
    }

    public static void PrintMessage(string message, bool newLine = true)
    {
        if (newLine)
            Console.WriteLine(message);
        else
            Console.Write(message);
    }

    public static void PrintError(string message)
    {
        var originalColor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"[ERROR] {message}");
        Console.ForegroundColor = originalColor;
    }

    public static void PrintSuccess(string message)
    {
        var originalColor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"[SUCCESS] {message}");
        Console.ForegroundColor = originalColor;
    }

    public static void PrintWarning(string message)
    {
        var originalColor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"[WARNING] {message}");
        Console.ForegroundColor = originalColor;
    }

    public static void PrintInfo(string message)
    {
        var originalColor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"[INFO] {message}");
        Console.ForegroundColor = originalColor;
    }

    public static void PrintPlayerStatus(Player player)
    {
        Console.WriteLine($"\nPlayer: {player.Name} | Class: {player.Class}");
        Console.WriteLine($"Level: {player.Level} | Exp: {player.Experience}");
        Console.WriteLine($"HP: {player.Health}/{player.MaxHealth} | Mana: {player.Mana}/{player.MaxMana}");
        Console.WriteLine($"Gold: {player.Gold} | Inventory: {player.Inventory.CurrentWeight}/{player.Inventory.MaxWeight}");
    }

    public static void PrintInventory(Player player)
    {
        PrintSection("Inventory");
        if (player.Inventory.Items.Count == 0)
        {
            PrintMessage("Inventory is empty.");
            return;
        }

        int index = 1;
        foreach (var item in player.Inventory.Items)
        {
            string details = $"(Weight: {item.Weight}";
            if (item.HealAmount > 0)
            {
                details += $", Heal: {item.HealAmount}";
            }
            details += $", Type: {item.Type})";
            Console.WriteLine($"{index}. {item.Name} {details}");
            Console.WriteLine($"   {item.Description}");
            index++;
        }
    }

    public static void PrintMenu(string[] options)
    {
        PrintSection("Choose an action");
        for (int i = 0; i < options.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {options[i]}");
        }
    }

    public static void Pause(string message = "Press any key to continue...")
    {
        PrintMessage(message, false);
        Console.ReadKey(true);
    }
}
