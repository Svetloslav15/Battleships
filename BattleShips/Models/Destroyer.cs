namespace BattleShips.Models
{
    public class Destroyer : Ship
    {
        public Destroyer(GameBoard gameBoard) : base(Constants.DestroyerSize, gameBoard)
        {
        }
    }
}