﻿namespace Sudoku
{
    public class Location
    {
        public Location(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
        public int X { get; set; }
        public int Y { get; set; }

        public override string ToString()
        {
            return "X:" + X + ", Y:" + Y;
        }

    }
}
