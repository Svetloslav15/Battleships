namespace BattleShips.Contracts
{
    public interface IShip
    {
        int Length { get; }

        void TryToHit(string coordinates);

        void DrawShip();

        void CreateShip();

        bool IsDead();
    }
}