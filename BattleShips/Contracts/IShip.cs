namespace BattleShips.Contracts
{
    using BattleShips.Models;

    public interface IShip
    {
        bool TryToHit(Point point);

        bool IsDead();
    }
}