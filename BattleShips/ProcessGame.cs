namespace BattleShips
{
    using BattleShips.Contracts;
    using BattleShips.Models;

    using System;

    public class ProcessGame : IProccessGame
    {
        public bool IsGameOver { get; set; }

        public GameBoard GameBoard { get; set; }

        public int MovesCount { get; set; }

        private void DeleteLastCommandFromTheConsole()
        {
            Console.SetCursorPosition(0, 15);
            Console.Write(Constants.InputMessage);
            Console.Write(new string(' ', 1000));
            Console.SetCursorPosition(Constants.InputMessage.Length, 15);
        }

        public ProcessGame()
        {
            this.IsGameOver = false;
            this.GameBoard = GameBoard.GetInstance();
        }

        public void GameOver()
        {
            this.IsGameOver = true;
            Console.SetCursorPosition(0, 15);
            Console.Write(Constants.GameOverMessage, this.MovesCount);
        }

        public void ProcessInput()
        {
            while(!this.IsGameOver)
            {
                bool isValidInput = false;

                while (!isValidInput)
                {
                    this.DeleteLastCommandFromTheConsole();
                    string input = Console.ReadLine();
                    if (input.ToLower() == Constants.ShowCommand)
                    {
                        this.GameBoard.Show();
                    }
                    else
                    {
                        Point point = new Point(input);
                        if (!this.CheckUserInput(point))
                        {
                            continue;
                        } 

                        isValidInput = this.GameBoard.TryToHit(point);
                    }
                }
                this.MovesCount++;
                
                if (this.GameBoard.IsOver())
                {
                    this.GameOver();
                }
            }
        }

        public bool CheckUserInput(Point point)
        {
            if (point.Row < 0 || point.Row > 10 || point.Col < 0 || point.Col > 10)
            {
                return false;
            }

            return true;
        }

        public void StartGame()
        {
            this.IsGameOver = false;
            this.MovesCount = 0;
            this.ProcessInput();
        }
    }
}