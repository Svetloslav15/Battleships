namespace BattleShips.Contracts
{
    using BattleShips.Models;
    
    using System.Collections.Generic;

    public interface IGameBoard
    {
        void DrawGameBoard(char character, bool withShips);

        bool TryToHit(Point coordinates);

        void SetFilledCoordinates(Point point);

        bool IsPointFilled(Point point);

        HashSet<Point> GetFilledCoordinates();

        void Show();

        bool IsOver();
    }
}