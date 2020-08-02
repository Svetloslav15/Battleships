namespace BattleShips.Contracts
{
    public interface IProccessGame
    {
        void StartGame();

        void ProcessInput();

        void GameOver();
    }
}