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
        private readonly IList<Point> coordinatesMissed;

        private GameBoard(int boardSize)
        {
            this.boardSize = boardSize;
            this.filledCoordinates = new HashSet<Point>();
            this.coordinatesMissed = new List<Point>();

            this.DrawGameBoard(Constants.NoShot, false);
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

        public void DrawGameBoard(char character, bool withShips)
        {
            //Draw top border
            Console.SetCursorPosition(0, 0);
            for (int counter = 0; counter <= boardSize; counter++)
            {
                Console.Write($"{counter}");
            }
            Console.WriteLine();

            //Draw body
            for (char row = Constants.FirstRowLetter; row <= Constants.LastRowLetter; row++)
            {
                Console.Write($"{row}");
                for (int col = 1; col <= boardSize; col++)
                {
                    if (withShips && this.GetFilledCoordinates().Any(ship => ship.Row == (int)(row - Constants.FirstRowLetter) - 1 && ship.Col == col - 1))
                    {
                        Console.Write(Constants.ShotHit);
                    }
                    else
                    {
                        Console.Write($"{character}");
                    }
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

        public void AddPointToCoordinatesMissed(Point point)
        {
            this.coordinatesMissed.Add(point);
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

            bool isHitted = false;
            foreach (Ship ship in ships)
            {
                isHitted = ship.TryToHit(coordinates);
                if (isHitted)
                {
                    break;
                }
            }

            if (!isHitted)
            {
                this.AddPointToCoordinatesMissed(coordinates);
                Drawer.Draw(coordinates, Constants.ShotMiss);
            }

            return true;
        }

        public void Show()
        {
            this.DrawGameBoard(Constants.Space, true);
        }

        public bool IsOver()
        {
            return this.ships.Where(ship => ship.IsDead()).Count() == Constants.ShipsCount;
        }
    }
}