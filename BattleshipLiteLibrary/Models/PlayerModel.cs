namespace BattleshipLiteLibrary.Models;

public class PlayerModel
{
    public string PlayerName { get; set; }
    public List<GridSpotModel> ShipLocations { get; set; }
    public List<GridSpotModel> ShotGrid { get; set; }

}
