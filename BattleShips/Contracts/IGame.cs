namespace BattleShips.Contracts
{
    public interface IGame
    {
        void StartGame();

        void ProcessInput();

        void GameOver();
    }
}