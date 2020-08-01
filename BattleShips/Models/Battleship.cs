namespace BattleShips.Models
{
    public class Battleship : Ship
    {
        public Battleship(GameBoard gameBoard) : base(Constants.BattleShipSize, gameBoard)
        {
        }
    }
}