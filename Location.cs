namespace Info.Obak.Sudoku
{
    public class Location
    {
        public Location(int col, int row)
        {
            this.Column = col;
            this.Row = row;
        }
        public int Column { get; set; }
        public int Row { get; set; }

        public override string ToString()
        {
            return "Col:" + Column + ", Row:" + Row;
        }
    }
}
