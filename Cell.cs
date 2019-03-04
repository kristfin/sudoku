using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku
{
    public class Cell : Location
    {
        public Cell(int x, int y, int value) : base(x, y)
        {
            this.Value = value;
        }
        public Cell(Location loc, int value) : base(loc.X, loc.Y)
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
