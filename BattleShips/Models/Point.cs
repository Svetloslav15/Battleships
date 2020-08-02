namespace BattleShips.Models
{
    using System;

    public class Point
    {
        public int Row { get; set; }

        public int Col { get; set; }

        public Point(string coordinates)
        {
            coordinates = coordinates.ToUpper();
            this.ValidateCoordinates(coordinates);
        }

        public Point(int row, int col)
        {
            this.Row = row;
            this.Col = col;
        }

        private void ValidateCoordinates(string coordinates)
        {
            if (coordinates.Length > 3)
            {
                this.Col = -1;
                this.Row = -1;
                return;
            }
            if (coordinates[0] < Constants.FirstRowLetter || coordinates[0] > Constants.LastRowLetter)
            {
                this.Row = -1;
            }
            else
            {
                this.Row = (int)(char.ToUpper(coordinates[0]) - '@') - 1;
            }

            string[] validCols = new string[10] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };
            if (Array.Exists(validCols, col => col == coordinates.Substring(1)))
            {
                this.Col = int.Parse(coordinates.Substring(1)) - 1;
            }
            else
            {
                this.Col = -1;
            }
        }
    }
}