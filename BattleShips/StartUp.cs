namespace BattleShips
{
    using BattleShips.Services;

    class StartUp
    {
        static void Main()
        {
            GameBoardService gameBoardService = new GameBoardService(Constants.BoardSize);
            gameBoardService.DrawGameBoard();
        }
    }
}