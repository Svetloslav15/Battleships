namespace BattleShips.Contracts
{
    public interface IShip
    {
        void TryToHit(string coordinates);

        void DrawShip();

        bool IsDead();
    }
}