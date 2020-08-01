namespace BattleShips.Models
{
    using BattleShips.Contracts;
    using System;

    public abstract class Ship : IShip
    {
        public void DrawShip()
        {
            throw new NotImplementedException();
        }

        public bool IsDead()
        {
            throw new NotImplementedException();
        }

        public void TryToHit(string coordinates)
        {
            throw new NotImplementedException();
        }
    }
}
