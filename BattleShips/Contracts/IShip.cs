namespace BattleShips.Contracts
{
    using BattleShips.Models;

    public interface IShip
    {
        void TryToHit(Point coordinates);

        void DrawShip();

        bool IsDead();
    }
}