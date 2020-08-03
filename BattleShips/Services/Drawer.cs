namespace BattleShips.Services
{
    using BattleShips.Models;
    
    using System;

    public static class Drawer
    {
        //Draw a character on the console
        public static void Draw(Point point, char character)
        {
            Console.SetCursorPosition(point.Col + 1, point.Row + 1);
            Console.Write(character);
        }
    }
}