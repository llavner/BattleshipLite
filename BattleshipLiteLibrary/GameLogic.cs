using BattleshipLiteLibrary.Models;
using System.Reflection;

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
            string location = Console.ReadLine().ToUpper().Trim();

            bool isValidLocation = false;

            try
            {
                isValidLocation = GameLogic.PlaceShip(model, location);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error: {ex.Message}");
            }

            if (!isValidLocation)
            {
                Console.WriteLine("Not a valid location. Please try again.");
            }

        } while (model.ShipLocations.Count < 5);
    }

    public static bool PlayerStillActive(PlayerModel player)
    {
        bool isActive = false;

        foreach (var ship in player.ShipLocations)
        {
            if (ship.Status != Enums.GridSpotStatus.Sunk)
            {
                isActive = true;
            }
        }

        return isActive;
    }

    public static int GetShotCount(PlayerModel player)
    {
        int shotCount = 0;

        foreach (var shot in player.ShotGrid)
        {
            if (shot.Status != Enums.GridSpotStatus.Empty)
            {
                shotCount += 1;
            }
        }

        return shotCount;
    }
    public static bool PlaceShip(PlayerModel model, string location)
    {
        bool output = false;

        (string row, int column) = SplitShotIntoRowAndColumn(location);

        bool isValidLocation = ValidateGridLocation(model, row, column);
        bool isSpotOpen = ValidateShipLocation(model, row, column);

        if (isValidLocation && isSpotOpen)
        {
            model.ShipLocations.Add(new GridSpotModel
            {
                SpotLetter = row,
                SpotNumber = column,
                Status = Enums.GridSpotStatus.Ship
            });

            output = true;
        }

        return output;
    }

    private static bool ValidateShipLocation(PlayerModel model, string row, int column)
    {
        bool isValidLocation = true;

        foreach (var ship in model.ShipLocations)
        {
            if (ship.SpotLetter == row.ToUpper() && ship.SpotNumber == column)
            {
                isValidLocation = false;
            }
        }

        return isValidLocation;
    }

    private static bool ValidateGridLocation(PlayerModel model, string row, int column)
    {
        bool isValidGridLocation = false;

        foreach (var shot in model.ShotGrid)
        {
            if (shot.SpotLetter == row.ToUpper() && shot.SpotNumber == column)
            {
                isValidGridLocation = true;
            }
        }

        return isValidGridLocation;
    }

    public static (string row, int column) SplitShotIntoRowAndColumn(string shot)
    {
        string row = string.Empty;
        int column = 0;

        if (shot.Length != 2)
        {
            throw new ArgumentException("This was an invalid shot type", nameof(shot));
        }

        char[] shotArray = shot.ToArray();

        row = shotArray[0].ToString();

        column = int.Parse(shotArray[1].ToString());

        return (row, column);

    }

    public static bool ValidateShot(PlayerModel player, string row, int column)
    {
        bool isValidShot = false;

        foreach (var gridSpot in player.ShotGrid)
        {
            if (gridSpot.SpotLetter == row.ToUpper() && gridSpot.SpotNumber == column)
            {
                if (gridSpot.Status == Enums.GridSpotStatus.Empty)
                {
                    isValidShot = true;
                }
            }
        }

        return isValidShot;
    }

    public static bool IdentifyShotResult(PlayerModel opponent, string row, int column)
    {
        bool isAHit = false;

        foreach (var ship in opponent.ShipLocations)
        {
            if (ship.SpotLetter == row.ToUpper() && ship.SpotNumber == column)
            {
                isAHit = true;
                ship.Status = Enums.GridSpotStatus.Sunk;
            }
        }

        return isAHit;
    }

    public static void MarkShotResult(PlayerModel player, string row, int column, bool isAHit)
    {

        foreach (var gridSpot in player.ShotGrid)
        {
            if (gridSpot.SpotLetter == row.ToUpper() && gridSpot.SpotNumber == column)
            {
                if (isAHit)
                {
                    gridSpot.Status = Enums.GridSpotStatus.Hit;
                }
                else
                {
                    gridSpot.Status = Enums.GridSpotStatus.Miss;
                }

            }
        }
    }
}
