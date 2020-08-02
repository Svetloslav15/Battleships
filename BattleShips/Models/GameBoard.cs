namespace BattleShips.Models
{
    using BattleShips.Contracts;
    using BattleShips.Services;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class GameBoard : IGameBoard
    {
        private readonly int boardSize;
        private static GameBoard gameBoardInstance;

        private readonly HashSet<Point> filledCoordinates;
        private readonly IList<Ship> ships;
        private readonly IList<Point> coordinatesHitted;
        private readonly IList<Point> coordinatesMissed;

        private GameBoard(int boardSize)
        {
            this.boardSize = boardSize;
            this.filledCoordinates = new HashSet<Point>();
            this.coordinatesHitted = new List<Point>();
            this.coordinatesMissed = new List<Point>();

            this.DrawGameBoard(Constants.NoShot);
            this.ships = new List<Ship>()
            {
                new Battleship(this),
                new Destroyer(this),
                new Destroyer(this),
            };
        }

        public static GameBoard GetInstance()
        {
            if (gameBoardInstance == null)
            {
                gameBoardInstance = new GameBoard(Constants.BoardSize);
            }
            return gameBoardInstance;
        }

        public void DrawGameBoard(char character)
        {
            for (int counter = 0; counter <= boardSize; counter++)
            {
                Console.Write($"{counter}");
            }
            Console.WriteLine();
            for (char row = 'A'; row <= 'J'; row++)
            {
                Console.Write($"{row}");
                for (int col = 1; col <= boardSize; col++)
                {
                    Console.Write($"{character}");
                }
                Console.WriteLine();
            }
        }

        public HashSet<Point> GetFilledCoordinates()
        {
            return this.filledCoordinates;
        }

        public void SetFilledCoordinates(Point point)
        {
            this.filledCoordinates.Add(point);
        }

        public bool IsPointFilled(Point point)
        {
            return this.filledCoordinates.FirstOrDefault(currPoint => currPoint.Row == point.Row && currPoint.Col == point.Col) != null;
        }

        public bool TryToHit(Point coordinates)
        {
            if (this.coordinatesMissed.Any(point => point.Row == coordinates.Row && point.Col == coordinates.Col))
            {
                return false;
            }

            if (this.filledCoordinates.Any(point => point.Row == coordinates.Row && point.Col == coordinates.Col))
            {
                this.filledCoordinates.Add(coordinates);
                Drawer.Draw(coordinates, Constants.ShotHit);
            }
            else
            {
                this.coordinatesMissed.Add(coordinates);
                Drawer.Draw(coordinates, Constants.ShotMiss);
            }

            return true;
        }

        public void Show()
        {
            this.DrawGameBoard(Constants.Space);
        }
    }
}
