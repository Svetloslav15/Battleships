namespace BattleShips
{
    using BattleShips.Models;
    using System.Collections.Generic;
    using System;

    class StartUp
    {
        static void Main()
        {
            GameBoard gameBoard = GameBoard.GetInstance();

            Console.SetCursorPosition(0, 20);
        }
    }
}