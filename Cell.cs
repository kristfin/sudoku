using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku
{
    public class Cell : Location
    {
        public Cell(int col, int row, int value) : base(col, row)
        {
            this.Value = value;
        }
        public Cell(Location loc, int value) : base(loc.Column, loc.Row)
        {
            this.Value = value;
        }
        public int Value { get; set; } = 0;

        public bool IsEmpty => Value == 0;

        public override string ToString()
        {
            return base.ToString() + ", Value:" + Value;
        }
    }
}
