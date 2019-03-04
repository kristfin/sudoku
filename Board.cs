using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku
{
    public class Board
    {
        public int Size { get; set; }
        int[,] board;

        public Board(int size)
        {
            this.Size = size;
            this.board = new int[(size * size), (size*size)];
        }
        
        public int Get(int x, int y)
        {            
            return board[y, x];
        }
        public void Set(int x, int y, int value)
        {
            board[y, x] = value;
        }

        public override string ToString()
        {
            int quad = Size * Size * Size * Size;
            int pad = (int)Math.Log10(quad) ;
            var hline = new string('-', (pad+1) * Size * Size + (Size+1)* 2 - 1 );
            var s = hline + "\n";
            for (int i = 0; i < Size * Size; i++)
            {
                s += "| ";
                for (int j = 0; j < Size * Size; j++)
                {
                    int val = Get(i, j);
                    s += val.ToString().PadLeft(pad) + " " + ((j + 1) % Size == 0 ? "| " : "");
                }
                s += "\n" + ((i + 1) % Size == 0 ? hline + "\n" : "");
            }
            return s;
        }
    }
}
