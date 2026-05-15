namespace CLIAirpg.UI;

public static class InputHandler
{
    public static string GetPlayerName()
    {
        ConsoleRenderer.PrintMessage("Enter your character name: ", false);
        var name = Console.ReadLine();
        return string.IsNullOrWhiteSpace(name) ? "Hero" : name;
    }

    public static int GetPlayerClass()
    {
        ConsoleRenderer.PrintSection("Select your class");
        var classes = Enum.GetValues(typeof(CharacterClass));
        int index = 1;
        foreach (CharacterClass c in classes)
        {
            Console.WriteLine($"{index}. {c}");
            index++;
        }

        ConsoleRenderer.PrintMessage("Enter your choice (1-4): ", false);
        return GetValidInput(1, 4);
    }

    public static int GetMenuChoice(int maxOption)
    {
        ConsoleRenderer.PrintMessage("Enter your choice: ", false);
        return GetValidInput(1, maxOption);
    }

    private static int GetValidInput(int min, int max)
    {
        while (true)
        {
            if (int.TryParse(Console.ReadLine(), out int choice) && choice >= min && choice <= max)
            {
                return choice;
            }
            ConsoleRenderer.PrintError($"Invalid input. Please enter a number between {min} and {max}.");
            ConsoleRenderer.PrintMessage("Try again: ", false);
        }
    }

    public static string GetActionInput()
    {
        ConsoleRenderer.PrintMessage("Enter action name or number: ", false);
        var input = Console.ReadLine();
        return string.IsNullOrWhiteSpace(input) ? "look" : input.ToLower().Trim();
    }
}
