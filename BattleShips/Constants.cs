namespace BattleShips
{
    public static class Constants
    {
        public const int BoardSize = 10;
        public const int DestroyerSize = 4;
        public const int BattleShipSize = 5;
        public const int ShipsCount = 3;
        
        public const char NoShot = '.';
        public const char ShotMiss = '-';
        public const char ShotHit = 'X';
        public const char Space = ' ';

        public const char FirstRowLetter = 'A';
        public const char LastRowLetter = 'J';

        public const string InputMessage = "Enter coordinates (row, col), e.g. A5 = ";
        public const string GameOverMessage = "Well done! You completed the game in {0} shots";
        public const string ShowCommand = "show";
    }
}