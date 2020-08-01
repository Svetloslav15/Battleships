namespace BattleShips.Services
{
    using BattleShips.Contracts;
    using BattleShips.Enums;
    using BattleShips.Models;
    
    using System;
    using System.Collections.Generic;

    public static class RandomService
    {
        public static Point GeneratePoint()
        {
            Random random = new Random();
            int row = random.Next(1, 11);
            int col = random.Next('A', 'J');
            return new Point($"{(char)col}{row}");
        }

        public static HashSet<Direction> GenerateSequenceOfDirections()
        {
            HashSet<Direction> directions = new HashSet<Direction>();
            const int maxDirectionsCount = 4;

            while(directions.Count < maxDirectionsCount)
            {
                Random random = new Random();
                int newNum = random.Next(0, maxDirectionsCount);

                switch (newNum)
                {
                    case 0: directions.Add(Direction.Top); break;
                    case 1: directions.Add(Direction.Right); break;
                    case 2: directions.Add(Direction.Bottom); break;
                    case 3: directions.Add(Direction.Left); break;
                }
            }

            return directions;
        }
    }
}