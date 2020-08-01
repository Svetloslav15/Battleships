namespace BattleShips.Models
{
    public class Point
    {
        public int Row { get; set; }

        public int Col { get; set; }

        public Point(string coordinates)
        {
            this.ParseCoorindates(coordinates);
        }

        private void ParseCoorindates(string coordinates)
        {
            this.Row = (int)(coordinates[0] - '@');
            this.Col = int.Parse(coordinates.Substring(1));
        }
    }
}