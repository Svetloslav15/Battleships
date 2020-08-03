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

        //Generating coordindates of the ship
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
                    //Generate first point of the ship and the order of directions we will try to create the body of the ship
                    directions = RandomService.GenerateSequenceOfDirections();
                    firstPoint = RandomService.GeneratePoint();
                }
                
                //Choose the direction in which the computer will try to create a ship
                Direction direction = directions.First();
                switch (direction)
                {
                    case Direction.Top:
                        {
                            //Check if there is enough space on the top for the ship
                            if (firstPoint.Row - this.Size >= 0)
                            {
                                int counter = 0;
                                bool isSpaceAvailable = true;
                                List<Point> points = new List<Point>();

                                //Check if any of the points is overlaping
                                for (int row = firstPoint.Row; counter++ < this.Size; row--)
                                {
                                    Point point = new Point(row, firstPoint.Col);
                                    points.Add(point);

                                    if (this.gameBoard.IsPointFilled(point))
                                    {
                                        isSpaceAvailable = false;
                                        break;
                                    }
                                }
                                if (isSpaceAvailable)
                                {
                                    this.AddPoints(points);
                                }
                            }
                        }
                        break;
                    case Direction.Right:
                        {
                            //Check if there is enough space on the right for the ship
                            if (firstPoint.Col + this.Size < Constants.BoardSize)
                            {
                                int counter = 0;
                                bool isSpaceAvailable = true;
                                List<Point> points = new List<Point>();

                                //Check if any of the points is overlaping
                                for (int col = firstPoint.Col; counter++ < this.Size; col++)
                                {
                                    Point point = new Point(firstPoint.Row, col);
                                    points.Add(point);
                                    
                                    if (this.gameBoard.IsPointFilled(point))
                                    {
                                        isSpaceAvailable = false;
                                        break;
                                    }
                                }
                                if (isSpaceAvailable)
                                {
                                    this.AddPoints(points);
                                }
                            }
                        }
                        break;
                    case Direction.Bottom:
                        {
                            //Check if there is enough space at the bottom for the ship
                            if (firstPoint.Row + 1 + this.Size < Constants.BoardSize)
                            {
                                int counter = 0;
                                bool isSpaceAvailable = true;
                                List<Point> points = new List<Point>();

                                //Check if any of the points is overlaping
                                for (int row = firstPoint.Row; counter++ < this.Size; row++)
                                {
                                    Point point = new Point(row, firstPoint.Col);
                                    points.Add(point);

                                    if (this.gameBoard.IsPointFilled(point))
                                    {
                                        isSpaceAvailable = false;
                                        break;
                                    }
                                }
                                if (isSpaceAvailable)
                                {
                                    this.AddPoints(points);
                                }
                            }
                        }
                        break;
                    case Direction.Left:
                        {
                            //Check if there is enough space on the left for the ship
                            if (firstPoint.Col - this.Size >= 0)
                            {
                                int counter = 0;
                                bool isSpaceAvailable = true;
                                List<Point> points = new List<Point>();

                                //Check if any of the points is overlaping
                                for (int col = firstPoint.Col; counter++ < this.Size; col--)
                                {
                                    Point point = new Point(firstPoint.Row, col);
                                    points.Add(point);
                                    if (this.gameBoard.IsPointFilled(point))
                                    {
                                        isSpaceAvailable = false;
                                        break;
                                    }
                                }
                                if (isSpaceAvailable)
                                {
                                    this.AddPoints(points);
                                }
                            }
                        }
                        break;
                }
                //Remove the first direction from the list
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

        //Add coordinates to the ship
        private void AddPoints(List<Point> points)
        {
            foreach (Point point in points)
            {
                this.coordinates.Add(point);
                this.gameBoard.SetFilledCoordinates(point);
            }
        }

        public bool TryToHit(Point coordinates)
        {
            //Check if the user is hitting a ship
            if (this.coordinates.Any(point => point.Row == coordinates.Row && point.Col == coordinates.Col))
            {
                //Draw that the user has hit a part of the ship
                this.coordinatesHitted.Add(coordinates);
                Drawer.Draw(coordinates, Constants.ShotHit);

                return true;
            }

            return false;
        }

        //Check if all parts of the ship are hit
        public bool IsDead()
        {
            return this.coordinates.Count == this.coordinatesHitted.Count;
        }
    }
}