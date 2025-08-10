using BattleshipLiteLibrary;
using BattleshipLiteLibrary.Models;
namespace BattleshipLite;

public static class ConsoleLogic
{
    public static PlayerModel CreatePlayer(string playerTitle)
    {
        PlayerModel output = new();

        Console.WriteLine($"This is {playerTitle}");

        output.PlayerName = AskForPlayersName();

        GameLogic.InitializeGrid(output);

        GameLogic.PlaceShips(output);

        Console.Clear();

        return output;
    }

    public static string AskForPlayersName()
    {
        Console.Write("Name: ");
        string output = Console.ReadLine();

        return output;
    }

    public static void DisplayShotGrid(PlayerModel activePlayer)
    {
        string currentRow = activePlayer.ShotGrid[0].SpotLetter;

        foreach (var gridsSpot in activePlayer.ShotGrid)
        {
            if (gridsSpot.SpotLetter != currentRow)
            {
                Console.WriteLine();
                currentRow = gridsSpot.SpotLetter;
            }

            if (gridsSpot.Status == Enums.GridSpotStatus.Empty)
            {
                Console.Write($" {gridsSpot.SpotLetter}{gridsSpot.SpotNumber} ");
            }
            else if (gridsSpot.Status == Enums.GridSpotStatus.Hit)
            {
                Console.Write(" X  ");
            }
            else if (gridsSpot.Status == Enums.GridSpotStatus.Miss)
            {
                Console.Write(" O  ");
            }
            else
            {
                Console.Write(" ?  ");
            }
        }

        Console.WriteLine();
        Console.WriteLine();
    }

    public static void RecordPlayerShot(PlayerModel activePlayer, PlayerModel opponent)
    {
        bool isValidShot = false;
        string row = string.Empty;
        int column = 0;

        do
        {
            string shot = AskForShot(activePlayer);

            try
            {
                (row, column) = GameLogic.SplitShotIntoRowAndColumn(shot);
                isValidShot = GameLogic.ValidateShot(activePlayer, row, column);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                isValidShot = false;
            }

            if (isValidShot == false)
            {
                Console.WriteLine("Invalid shot location, please try again.");
            }

        } while (isValidShot == false);

        bool isAHit = GameLogic.IdentifyShotResult(opponent, row, column);

        GameLogic.MarkShotResult(activePlayer, row, column, isAHit);

        DisplayShotResults(row, column, isAHit);
    }

    private static void DisplayShotResults(string row, int column, bool isAHit)
    {
        if (isAHit)
        {
            Console.WriteLine($"{row}{column} is a hit!");
        }
        else
        {
            Console.WriteLine($"{row}{column} is a miss!");
        }

        Console.WriteLine();
    }

    private static string AskForShot(PlayerModel activePlayer)
    {
        Console.Write($"{activePlayer.PlayerName}, please enter your shot selection: ");
        string output = Console.ReadLine().ToUpper().Trim();

        return output;
    }

    public static void IdentifyWinner(PlayerModel? winner)
    {
        Console.WriteLine($"Congratulations to {winner.PlayerName} for winning!");
        Console.WriteLine($"{winner.PlayerName} took { GameLogic.GetShotCount(winner) } shots.");
    }
}
