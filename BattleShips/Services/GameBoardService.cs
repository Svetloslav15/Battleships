namespace BattleShips.Services
{
    using BattleShips.Contracts;
    using System;

    public class GameBoardService : IGameBoardService
    {
        private int boardSize;

        public GameBoardService(int boardSize)
        {
            this.boardSize = boardSize;
        }

        public void DrawGameBoard()
        {
            //Draw border up
            for (int counter = 0; counter <= boardSize; counter++)
            {
                Console.Write($"{counter} ");
            }
            Console.WriteLine();
            for (char row = 'A'; row <= 'J'; row++)
            {
                Console.Write($"{row} ");
                for (int col = 1; col <= boardSize; col++)
                {
                    Console.Write($". ");
                }
                Console.WriteLine();
            }
        }
    }
}