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

        //Get the instance of the GameBoard
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
                //Draw the left border
                Console.Write($"{row}");

                //Drawing the body of the gameboard
                for (int col = 1; col <= boardSize; col++)
                {
                    //If the command is "show" and there is a ship on this point, it will be shown
                    if (withShips && this.GetFilledCoordinates().Any(ship => ship.Row == (int)(row - Constants.FirstRowLetter) - 1 && ship.Col == col - 1))
                    {
                        Console.Write(Constants.ShotHit);
                    }
                    //Draw the point on the board
                    else
                    {
                        Console.Write($"{character}");
                    }
                }
                //Go on the next row
                Console.WriteLine();
            }
        }

        //Get coordinates with ships on them
        public HashSet<Point> GetFilledCoordinates()
        {
            return this.filledCoordinates;
        }

        //Add coordinates of a ship
        public void SetFilledCoordinates(Point point)
        {
            this.filledCoordinates.Add(point);
        }

        //Add coordinates of the Point user missed
        public void AddPointToCoordinatesMissed(Point point)
        {
            this.coordinatesMissed.Add(point);
        }

        //Check if there is a ship on the Point
        public bool IsPointFilled(Point point)
        {
            return this.filledCoordinates.FirstOrDefault(currPoint => currPoint.Row == point.Row && currPoint.Col == point.Col) != null;
        }

        //Process user move
        public bool TryToHit(Point coordinates)
        {
            //Check if the user has tried to hit on these coordinates
            if (this.coordinatesMissed.Any(point => point.Row == coordinates.Row && point.Col == coordinates.Col))
            {
                return false;
            }

            bool isHitted = false;
            //Check if any of the ships is hit
            foreach (Ship ship in ships)
            {
                isHitted = ship.TryToHit(coordinates);
                if (isHitted)
                {
                    break;
                }
            }

            //Draw if the user has missed to hit a ship
            if (!isHitted)
            {
                this.AddPointToCoordinatesMissed(coordinates);
                Drawer.Draw(coordinates, Constants.ShotMiss);
            }

            return true;
        }
        
        //Show where are the ships on the board
        public void Show()
        {
            this.DrawGameBoard(Constants.Space, true);
        }

        //Check if all ships are dead
        public bool IsOver()
        {
            return this.ships.Where(ship => ship.IsDead()).Count() == Constants.ShipsCount;
        }
    }
}