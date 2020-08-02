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

        public ProcessGame()
        {
            this.IsGameOver = false;
            this.GameBoard = GameBoard.GetInstance();
        }

        public void GameOver()
        {
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
                    Console.SetCursorPosition(0, 15);
                    Console.Write(Constants.InputMessage);
                    Console.Write("    ");
                    Console.SetCursorPosition(Constants.InputMessage.Length, 15);
                    string input = Console.ReadLine();
                    if (input == "show")
                    {
                        this.GameBoard.Show();
                    }
                    else
                    {
                        isValidInput = this.GameBoard.TryToHit(new Point(input));
                    }
                }
                this.MovesCount++;
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