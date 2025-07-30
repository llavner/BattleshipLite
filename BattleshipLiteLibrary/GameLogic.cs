using BattleshipLiteLibrary.Models;

namespace BattleshipLiteLibrary;

public class GameLogic
{
    public static void InitializeGrid(PlayerModel model)
    {
        List<string> letters = new()
        {
            "A",
            "B",
            "C",
            "D",
            "E"
        };

        List<int> numbers = new()
        {
            1,
            2,
            3,
            4,
            5
        };

        foreach (var letter in letters)
        {
            foreach (var number in numbers)
            {
                AddGridSpot(model, letter, number);
            }
        }
    }

    public static bool PlaceShip(PlayerModel model, string location)
    {
        throw new NotImplementedException();
    }

    private static void AddGridSpot(PlayerModel model, string letter, int number)
    {
        GridSpotModel spot = new()
        {
            SpotLetter = letter,
            SpotNumber = number,
            Status = Enums.GridSpotStatus.Empty
        };

        model.ShotGrid.Add(spot);
    }

    public static void PlaceShips(PlayerModel model)
    {
        do
        {
            Console.Write($"Where do you want to place ship number {model.ShipLocations.Count + 1}: ");
            string location = Console.ReadLine();

            bool isValidLocation = GameLogic.PlaceShip(model, location);

            if (!isValidLocation)
            {
                Console.WriteLine("Not a valid location. Please try again.");
            }

        } while (model.ShipLocations.Count < 5);
    }

    public static bool PlayerStillActive(PlayerModel opponent)
    {
        throw new NotImplementedException();
    }

    public static int GetShotCount(PlayerModel winner)
    {
        throw new NotImplementedException();
    }

    public static (string row, int column) SplitShotIntoRowAndColumn(string shot)
    {
        throw new NotImplementedException();
    }

    public static bool ValidateShot(PlayerModel activePlayer, string row, int column)
    {
        throw new NotImplementedException();
    }

    public static bool IdentifyShotResult(PlayerModel opponent, string row, int column)
    {
        throw new NotImplementedException();
    }

    public static void MarkShotResult(PlayerModel activePlayer, string row, int column, bool isAHit)
    {
        throw new NotImplementedException();
    }
}
