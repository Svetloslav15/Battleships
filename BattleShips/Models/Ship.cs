namespace BattleShips.Models
{
    using BattleShips.Contracts;
    using BattleShips.Enums;
    using BattleShips.Services;

    using System;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class Ship : IShip
    {
        public int Size { get; }

        private readonly IList<Point> coordinates;
        private GameBoard gameBoard;

        private void CreateShip()
        {
            HashSet<Direction> directions = RandomService.GenerateSequenceOfDirections();

            //Generate starting point for the ship
            Point firstPoint = RandomService.GeneratePoint();
            while (this.gameBoard.IsPointFilled(firstPoint))
            {
                firstPoint = RandomService.GeneratePoint();
            }
            
            //Generate the body of the ship
            while (this.coordinates.Count < this.Size)
            {
                if (directions.Count == 0)
                {
                    directions = RandomService.GenerateSequenceOfDirections();
                    firstPoint = RandomService.GeneratePoint();
                }
                Direction direction = directions.First();

                switch (direction)
                {
                    case Direction.Top:
                        {
                            if (firstPoint.Row - this.Size >= 0)
                            {
                                int counter = 0;
                                bool isSpaceAvailable = true;

                                for (int row = firstPoint.Row; counter++ < this.Size; row--)
                                {
                                    Point point = new Point(row, firstPoint.Col);
                                    if (this.gameBoard.IsPointFilled(point))
                                    {
                                        isSpaceAvailable = false;
                                        break;
                                    }
                                }
                                if (isSpaceAvailable)
                                {
                                    for (int row = firstPoint.Row; this.coordinates.Count < this.Size; row--)
                                    {
                                        this.AddPoint(row, firstPoint.Col);
                                    }
                                }
                            }
                        }
                        break;
                    case Direction.Right:
                        {
                            if (firstPoint.Col + this.Size < Constants.BoardSize)
                            {
                                int counter = 0;
                                bool isSpaceAvailable = true;
                                for (int col = firstPoint.Col; counter++ < this.Size; col++)
                                {
                                    Point point = new Point(firstPoint.Row, col);
                                    if (this.gameBoard.IsPointFilled(point))
                                    {
                                        isSpaceAvailable = false;
                                        break;
                                    }
                                }
                                if (isSpaceAvailable)
                                {
                                    for (int col = firstPoint.Col; this.coordinates.Count < this.Size; col++)
                                    {
                                        this.AddPoint(firstPoint.Row, col);
                                    }
                                }
                            }
                        }
                        break;
                    case Direction.Bottom:
                        {
                            if (firstPoint.Row + 1 + this.Size < Constants.BoardSize)
                            {
                                int counter = 0;
                                bool isSpaceAvailable = true;
                                for (int row = firstPoint.Row; counter++ < this.Size; row++)
                                {
                                    Point point = new Point(row, firstPoint.Col);
                                    if (this.gameBoard.IsPointFilled(point))
                                    {
                                        isSpaceAvailable = false;
                                        break;
                                    }
                                }
                                if (isSpaceAvailable)
                                {
                                    for (int row = firstPoint.Row; this.coordinates.Count < this.Size; row++)
                                    {
                                        this.AddPoint(row, firstPoint.Col);
                                    }
                                }
                            }
                        }
                        break;
                    case Direction.Left:
                        {
                            if (firstPoint.Col - this.Size >= 0)
                            {
                                int counter = 0;
                                bool isSpaceAvailable = true;
                                for (int col = firstPoint.Col; counter++ < this.Size; col--)
                                {
                                    Point point = new Point(firstPoint.Row, col);
                                    if (this.gameBoard.IsPointFilled(point))
                                    {
                                        isSpaceAvailable = false;
                                        break;
                                    }
                                }
                                if (isSpaceAvailable)
                                {
                                    for (int col = firstPoint.Col; this.coordinates.Count < this.Size; col--)
                                    {
                                        this.AddPoint(firstPoint.Row, col);
                                    }
                                }
                            }
                        }
                        break;
                }
                directions.Remove(direction);
            }
        }

        public Ship(int size, GameBoard gameBoard)
        {
            this.Size = size;
            this.gameBoard = gameBoard;
            this.coordinates = new List<Point>();
            this.CreateShip();
            this.DrawShip();
        }

        public void DrawShip()
        {
            foreach (Point point in coordinates)
            {
                Console.SetCursorPosition(point.Col + 1, point. Row + 1);
                Console.Write(Constants.ShotHit);
            }
        }

        public bool IsDead()
        {
            throw new NotImplementedException();
        }

        public void TryToHit(Point coordinates)
        {
            throw new NotImplementedException();
        }

        private void AddPoint(int row, int col)
        {
            Point point = new Point(row, col);
            this.coordinates.Add(point);
            Console.SetCursorPosition(0, 30 + this.gameBoard.GetFilledCoordinates().Count);

            Console.WriteLine($"row: {point.Row} {point.Col} Count: {this.gameBoard.GetFilledCoordinates().Count}");
            this.gameBoard.SetFilledCoordinates(point);
        }
    }
}