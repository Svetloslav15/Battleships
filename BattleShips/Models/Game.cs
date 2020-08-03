namespace BattleShips
{
    using BattleShips.Contracts;
    using BattleShips.Models;

    using System;

    public class Game : IGame
    {
        public bool IsGameOver { get; set; }

        public GameBoard GameBoard { get; set; }

        public int MovesCount { get; set; }

        //Check if the user input is on the board
        private bool CheckUserInput(Point point)
        {
            if (point.Row < 0 || point.Row > Constants.BoardSize || point.Col < 0 || point.Col > Constants.BoardSize)
            {
                return false;
            }

            return true;
        }

        //Clear the last move of the user, in order to add new
        private void DeleteLastCommandFromTheConsole()
        {
            Console.SetCursorPosition(0, 15);
            Console.Write(Constants.InputMessage);
            Console.Write(new string(' ', 1000));
            Console.SetCursorPosition(Constants.InputMessage.Length, 15);
        }

        public Game()
        {
            this.IsGameOver = false;
            this.GameBoard = GameBoard.GetInstance();
        }

        //Finish the Game
        public void GameOver()
        {
            this.IsGameOver = true;
            Console.SetCursorPosition(0, 15);
            Console.Write(Constants.GameOverMessage, this.MovesCount);
        }

        //Read the user input from the console
        public void ProcessInput()
        {
            //Read the input until the game is finished
            while(!this.IsGameOver)
            {
                bool isValidInput = false;

                //Read the input until it is valid
                while (!isValidInput)
                {
                    this.DeleteLastCommandFromTheConsole();
                    string input = Console.ReadLine();

                    //Check if the user wants to see where are the ships
                    if (input.ToLower() == Constants.ShowCommand)
                    {
                        this.IsGameOver = true;
                        this.GameBoard.Show();
                        break;
                    }
                    else
                    {
                        //Check if the user hit a ship
                        Point point = new Point(input);
                        if (!this.CheckUserInput(point))
                        {
                            continue;
                        } 

                        isValidInput = this.GameBoard.TryToHit(point);
                    }
                }
                //Increment the user moves
                this.MovesCount++;
                
                if (this.GameBoard.IsOver())
                {
                    this.GameOver();
                }
            }
        }

        public void StartGame()
        {
            this.IsGameOver = false;
            this.MovesCount = 0;
            this.ProcessInput();
        }
    }
}