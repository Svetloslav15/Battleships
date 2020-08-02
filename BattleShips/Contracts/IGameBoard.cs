namespace BattleShips.Contracts
{
    using BattleShips.Models;
    
    using System.Collections.Generic;

    public interface IGameBoard
    {
        void DrawGameBoard(char character, bool withShips);

        void SetFilledCoordinates(Point point);

        bool TryToHit(Point coordinates);

        bool IsOver();

        bool IsPointFilled(Point point);

        HashSet<Point> GetFilledCoordinates();

        void Show();
    }
}