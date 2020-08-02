namespace BattleShips.Services
{
    using BattleShips.Enums;
    using BattleShips.Models;
    
    using System;
    using System.Collections.Generic;

    public static class RandomService
    {
        //Generate random point on the gameboard
        public static Point GeneratePoint()
        {
            Random random = new Random();
            int row = random.Next(1, Constants.BoardSize + 1);
            int col = random.Next(Constants.FirstRowLetter, Constants.LastRowLetter);
            return new Point($"{(char)col}{row}");
        }

        //Generate sequence of directions with random order
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