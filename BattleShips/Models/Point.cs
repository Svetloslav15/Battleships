namespace BattleShips.Models
{
    public class Point
    {
        public int Row { get; set; }

        public int Col { get; set; }

        public Point(string coordinates)
        {
            this.Row = (int)(coordinates[0] - '@') - 1;
            this.Col = int.Parse(coordinates.Substring(1)) - 1;
        }

        public Point(int row, int col)
        {
            this.Row = row;
            this.Col = col;
        }
    }
}