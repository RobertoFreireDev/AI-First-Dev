using CLIAirpg.Core;

namespace CLIAirpg;

class Program
{
    static void Main(string[] args)
    {
        var gameLoop = new GameLoop();
        gameLoop.Start();
    }
}
