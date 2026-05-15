using CLIAirpg.Core;
using CLIAirpg.Systems;
using CLIAirpg.UI;

namespace CLIAirpg.Core;

public class GameLoop
{
    private GameState _gameState;
    private bool _shouldExit;

    public GameLoop()
    {
        _gameState = new GameState();
        _shouldExit = false;
    }

    public void Start()
    {
        ConsoleRenderer.ClearScreen();
        ConsoleRenderer.PrintTitle("CLI AIR RPG - Version 0.2");

        // Initialize game
        InitializeGame();

        if (_gameState.Player == null)
        {
            return;
        }

        _gameState.IsGameRunning = true;

        // Main game loop
        MainLoop();

        // Game ended
        ConsoleRenderer.PrintMessage("\n\nThanks for playing CLI AIR RPG!");
        ConsoleRenderer.Pause();
    }

    private void InitializeGame()
    {
        // Create player
        var playerName = InputHandler.GetPlayerName();
        int classChoice = InputHandler.GetPlayerClass();
        var characterClass = (CharacterClass)(classChoice - 1);

        var player = new Player(playerName, characterClass);
        _gameState.Player = player;

        // Initialize starting location
        InitializeLocations();
        _gameState.CurrentLocation = _gameState.GetLocation("starting_area");

        // Add starting items
        var dagger = new Item("dagger", "Iron Dagger", "A basic iron dagger", 5);
        player.Inventory.AddItem(dagger);

        var potion = new Item("health_potion", "Health Potion", "Restores 30 HP", 2, value: 5, healAmount: 30, type: "Consumable");
        player.Inventory.AddItem(potion);

        var leatherArmor = new Item("leather_armor", "Leather Armor", "Light armor that slightly reduces damage.", 10, value: 12, type: "Armor");
        player.Inventory.AddItem(leatherArmor);

        ConsoleRenderer.ClearScreen();
        ConsoleRenderer.PrintSuccess($"Welcome, {player.Name} the {player.Class}!");
        ConsoleRenderer.PrintMessage($"You wake up in {_gameState.CurrentLocation?.Name}.");
        ConsoleRenderer.Pause();
    }

    private void InitializeLocations()
    {
        // Create starting area
        var startingArea = new Location("starting_area", "Starting Area", 
            "A peaceful village square with old stone buildings around you.");
        startingArea.ConnectTo("forest");
        _gameState.AddLocation(startingArea);

        // Create forest
        var forest = new Location("forest", "Dark Forest", 
            "A dense forest with tall trees blocking out the sunlight.");
        forest.ConnectTo("starting_area");
        forest.ConnectTo("cave");
        _gameState.AddLocation(forest);

        // Create cave
        var cave = new Location("cave", "Mysterious Cave", 
            "A dark cave entrance with an eerie feeling emanating from within.");
        cave.ConnectTo("forest");
        _gameState.AddLocation(cave);
    }

    private void MainLoop()
    {
        while (_gameState.IsGameRunning && !_shouldExit)
        {
            ConsoleRenderer.ClearScreen();
            DisplayGameStatus();

            string action = InputHandler.GetActionInput();
            ProcessAction(action);

            _gameState.IncrementTurn();
        }
    }

    private void DisplayGameStatus()
    {
        if (_gameState.Player == null || _gameState.CurrentLocation == null)
            return;

        ConsoleRenderer.PrintTitle($"Turn {_gameState.CurrentTurn} - {_gameState.CurrentLocation.Name}");
        ConsoleRenderer.PrintPlayerStatus(_gameState.Player);

        ConsoleRenderer.PrintSection("Location");
        ConsoleRenderer.PrintMessage(_gameState.CurrentLocation.Description);

        if (_gameState.CurrentLocation.ConnectedLocationIds.Count > 0)
        {
            Console.WriteLine("\nConnected locations:");
            foreach (var locId in _gameState.CurrentLocation.ConnectedLocationIds)
            {
                var location = _gameState.GetLocation(locId);
                Console.WriteLine($"  - {location?.Name}");
            }
        }

        ConsoleRenderer.PrintSection("Actions");
        Console.WriteLine("1. look - Look around (refresh description)");
        Console.WriteLine("2. explore - Search for enemies or hidden items");
        Console.WriteLine("3. inventory - Check your inventory");
        Console.WriteLine("4. use - Use a consumable item");
        Console.WriteLine("5. status - Full character status");
        Console.WriteLine("6. travel - Travel to connected location");
        Console.WriteLine("7. quit - Exit the game");
    }

    private void ProcessAction(string action)
    {
        if (_gameState.Player == null || _gameState.CurrentLocation == null)
            return;

        action = NormalizeAction(action);

        switch (action)
        {
            case "look":
                ConsoleRenderer.PrintMessage(_gameState.CurrentLocation.Description);
                ConsoleRenderer.Pause();
                break;

            case "explore":
                HandleExplore();
                break;

            case "inventory":
                ConsoleRenderer.PrintInventory(_gameState.Player);
                ConsoleRenderer.Pause();
                break;

            case "use":
                HandleUseItem();
                break;

            case "status":
                DisplayFullStatus();
                break;

            case "travel":
                HandleTravel();
                break;

            case "quit":
                HandleQuit();
                break;

            default:
                ConsoleRenderer.PrintError("Unknown action. Try: look, explore, inventory, use, status, travel, or quit");
                ConsoleRenderer.Pause();
                break;
        }
    }

    private void HandleTravel()
    {
        if (_gameState.CurrentLocation == null)
            return;

        if (_gameState.CurrentLocation.ConnectedLocationIds.Count == 0)
        {
            ConsoleRenderer.PrintError("There are no connected locations from here.");
            ConsoleRenderer.Pause();
            return;
        }

        ConsoleRenderer.PrintSection("Available Locations");
        int index = 1;
        foreach (var locId in _gameState.CurrentLocation.ConnectedLocationIds)
        {
            var location = _gameState.GetLocation(locId);
            Console.WriteLine($"{index}. {location?.Name}");
            index++;
        }

        int choice = InputHandler.GetMenuChoice(_gameState.CurrentLocation.ConnectedLocationIds.Count);
        var selectedLocId = _gameState.CurrentLocation.ConnectedLocationIds[choice - 1];
        var selectedLocation = _gameState.GetLocation(selectedLocId);

        if (selectedLocation != null)
        {
            _gameState.CurrentLocation = selectedLocation;
            ConsoleRenderer.ClearScreen();
            ConsoleRenderer.PrintSuccess($"You travel to {selectedLocation.Name}");
            TryLocationEncounter(selectedLocation);
            ConsoleRenderer.Pause();
        }
    }

    private void DisplayFullStatus()
    {
        if (_gameState.Player == null)
            return;

        var player = _gameState.Player;

        ConsoleRenderer.ClearScreen();
        ConsoleRenderer.PrintTitle("Character Status");

        Console.WriteLine($"Name: {player.Name}");
        Console.WriteLine($"Class: {player.Class}");
        Console.WriteLine($"Level: {player.Level}");
        Console.WriteLine($"Experience: {player.Experience}");
        Console.WriteLine($"Health: {player.Health}/{player.MaxHealth}");
        Console.WriteLine($"Mana: {player.Mana}/{player.MaxMana}");
        Console.WriteLine($"Gold: {player.Gold}");

        Console.WriteLine("\nAttributes:");
        Console.WriteLine($"  Strength: {player.Strength}");
        Console.WriteLine($"  Intelligence: {player.Intelligence}");
        Console.WriteLine($"  Dexterity: {player.Dexterity}");

        Console.WriteLine($"\nInventory ({player.Inventory.CurrentWeight}/{player.Inventory.MaxWeight}):");
        if (player.Inventory.Items.Count == 0)
        {
            Console.WriteLine("  Empty");
        }
        else
        {
            foreach (var item in player.Inventory.Items)
            {
                Console.WriteLine($"  - {item.Name}");
            }
        }

        ConsoleRenderer.Pause();
    }

    private void HandleExplore()
    {
        if (_gameState.CurrentLocation == null)
            return;

        var enemy = EncounterService.CreateEncounter(_gameState.CurrentLocation.Id);
        if (enemy == null)
        {
            ConsoleRenderer.PrintMessage("You search the area carefully but find nothing dangerous.");
            ConsoleRenderer.Pause();
            return;
        }

        ConsoleRenderer.ClearScreen();
        ConsoleRenderer.PrintWarning($"A wild {enemy.Name} appears!");
        var outcome = CombatEngine.EngageCombat(_gameState.Player, enemy);

        if (outcome == CombatOutcome.Victory)
        {
            AwardVictory(enemy);
        }
        else if (outcome == CombatOutcome.Defeat)
        {
            _gameState.IsGameRunning = false;
            _shouldExit = true;
        }
    }

    private void HandleUseItem()
    {
        if (_gameState.Player == null)
            return;

        var player = _gameState.Player;
        var potion = player.Inventory.Items.FirstOrDefault(item => item.Id == "health_potion" && item.HealAmount > 0);
        if (potion == null)
        {
            ConsoleRenderer.PrintError("You don't have a health potion to use.");
            ConsoleRenderer.Pause();
            return;
        }

        player.Heal(potion.HealAmount);
        player.Inventory.RemoveItem(potion.Id);
        ConsoleRenderer.PrintSuccess($"You drink a {potion.Name} and restore {potion.HealAmount} HP.");
        ConsoleRenderer.Pause();
    }

    private void TryLocationEncounter(Location location)
    {
        var enemy = EncounterService.CreateEncounter(location.Id);
        if (enemy == null)
        {
            return;
        }

        ConsoleRenderer.PrintWarning($"As you arrive, {enemy.Name} attacks!");
        var outcome = CombatEngine.EngageCombat(_gameState.Player, enemy);

        if (outcome == CombatOutcome.Victory)
        {
            AwardVictory(enemy);
        }
        else if (outcome == CombatOutcome.Defeat)
        {
            _gameState.IsGameRunning = false;
            _shouldExit = true;
        }
    }

    private void AwardVictory(Enemy enemy)
    {
        if (_gameState.Player == null)
            return;

        var player = _gameState.Player;
        player.GainExperience(enemy.ExperienceReward);
        player.AddGold(enemy.GoldReward);

        ConsoleRenderer.PrintSuccess($"You gained {enemy.ExperienceReward} experience and {enemy.GoldReward} gold.");

        if (enemy.Loot.Count > 0)
        {
            foreach (var item in enemy.Loot)
            {
                if (player.Inventory.AddItem(item))
                {
                    ConsoleRenderer.PrintSuccess($"You loot {item.Name}.");
                }
                else
                {
                    ConsoleRenderer.PrintWarning($"Your inventory is full and you leave {item.Name} behind.");
                }
            }
        }

        if (player.Level > 1)
        {
            ConsoleRenderer.PrintSuccess($"You reached level {player.Level}! Your power grows.");
        }

        ConsoleRenderer.Pause();
    }

    private string NormalizeAction(string action)
    {
        return action switch
        {
            "1" => "look",
            "2" => "explore",
            "3" => "inventory",
            "4" => "use",
            "5" => "status",
            "6" => "travel",
            "7" => "quit",
            _ => action
        };
    }

    private void HandleQuit()
    {
        ConsoleRenderer.PrintSection("Quit Game");
        ConsoleRenderer.PrintMessage("Are you sure you want to quit? (y/n): ", false);
        string response = Console.ReadLine() ?? "n";

        if (response.ToLower() == "y")
        {
            ConsoleRenderer.PrintSuccess("Game saved and exited.");
            _gameState.IsGameRunning = false;
            _shouldExit = true;
        }
    }
}
