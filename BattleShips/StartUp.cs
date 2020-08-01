namespace BattleShips
{
    using BattleShips.Services;
    using System;
    using System.Collections.Generic;

    class StartUp
    {
        static void Main()
        {
            GameBoardService gameBoardService = new GameBoardService(Constants.BoardSize);
            gameBoardService.DrawGameBoard();

            Console.WriteLine((int)('A'));
        }
    }
}