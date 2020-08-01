namespace BattleShips.Models
{
    using BattleShips.Contracts;
    using System;
    using System.Collections.Generic;

    public abstract class Ship : IShip
    {
        public int Length { get; }

        private IList<string> coordinates;

        public Ship(int length)
        {
            this.Length = length;
            this.coordinates = new List<string>();
        }

        public void DrawShip()
        {
            for (int i = 0; i < this.Length; i++)
            {
                Console.Write("*");
            }
            Console.WriteLine();
        }

        public bool IsDead()
        {
            throw new NotImplementedException();
        }

        public void TryToHit(string coordinates)
        {
            throw new NotImplementedException();
        }

        public void CreateShip()
        {
            throw new NotImplementedException();
        }
    }
}
