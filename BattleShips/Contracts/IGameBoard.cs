﻿namespace BattleShips.Contracts
{
    using BattleShips.Models;
    
    using System.Collections.Generic;

    public interface IGameBoard
    {
        void DrawGameBoard(char character);

        bool TryToHit(Point coordinates);

        void SetFilledCoordinates(Point point);

        bool IsPointFilled(Point point);

        HashSet<Point> GetFilledCoordinates();

        void Show();
    }
}