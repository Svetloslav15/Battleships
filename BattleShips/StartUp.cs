namespace BattleShips
{
    using BattleShips.Services;
    using BattleShips.Models;
    using System.Collections.Generic;
    using System;

    class StartUp
    {
        static void Main()
        {
            GameBoardService gameBoardService = new GameBoardService(Constants.BoardSize);
            gameBoardService.DrawGameBoard();

            List<Point> points = new List<Point>()
            {
                new Point("A1"),
                new Point("G10"),
                new Point("C7"),
                new Point("E8"),
                new Point("I1"),
            };

            foreach (var item in points)
            {
                Console.WriteLine($"Row: {item.Row}, Col: {item.Col}");
            }
        }
    }
}