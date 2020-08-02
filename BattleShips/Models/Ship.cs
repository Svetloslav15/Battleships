namespace BattleShips.Models
{
    using BattleShips.Contracts;
    using BattleShips.Enums;
    using BattleShips.Services;

    using System.Collections.Generic;
    using System.Linq;

    public abstract class Ship : IShip
    {
        public int Size { get; }

        private readonly IList<Point> coordinates;
        private readonly IList<Point> coordinatesHitted;

        private readonly GameBoard gameBoard;

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
            this.coordinatesHitted = new List<Point>();
            this.CreateShip();
        }

        private void AddPoint(int row, int col)
        {
            Point point = new Point(row, col);
            this.coordinates.Add(point);
            this.gameBoard.SetFilledCoordinates(point);
        }

        public bool TryToHit(Point coordinates)
        {
            if (this.coordinates.Any(point => point.Row == coordinates.Row && point.Col == coordinates.Col))
            {
                this.coordinatesHitted.Add(coordinates);
                Drawer.Draw(coordinates, Constants.ShotHit);

                return true;
            }

            return false;
        }

        public bool IsDead()
        {
            return this.coordinates.Count == this.coordinatesHitted.Count;
        }
    }
}