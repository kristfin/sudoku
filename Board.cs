using System;
using System.Collections.Generic;
using System.Text;

namespace Info.Obak.Sudoku
{
    public class Board
    {
        public int CellCount => Size * Size * Size * Size;
        public int HorizontalCellCount => Size * Size;
        public int VerticalCellCount => Size * Size;
        public int Size { get; set; }
        List<int[]> board;

        public Board(int size)
        {
            this.Size = size;
            Reset();
        }

        public void Reset()
        {             
            this.board = new List<int[]>();
            for (int i=0; i<Size*Size; i++)
            {
                this.board.Add(new int[Size*Size]);
            }
        }
        
        public int Get(int col, int row)
        {
            return board[row][col];
        }

        public void Set(int col, int row, int value)
        {
            board[row][col] = value;
        }

        public override string ToString()
        {
            return Render(null);
        }       

        public string Render(int[] deck)
        {
            int quad = Size * Size * Size * Size;
            int pad = (int)Math.Log10(quad);
            var s = "   " + new string(' ', pad);
            for (int i = 0; i < Size * Size; i++)
            {
                s += ("" + i).PadLeft(pad) + ((i + 1) % Size == 0 ? "   " : " ");
            }
            var hline = " " + new string(' ', pad) + new string('-', (pad + 1) * Size * Size + (Size + 1) * 2 - 1);
            s += "\n" + hline + "\n";
            for (int row = 0; row < Size * Size; row++)
            {
                s += ("" + row).PadLeft(pad) + " | ";
                for (int col = 0; col < Size * Size; col++)
                {
                    var val = "0123456789ABCDEFGHIJKLMNOPQRST"[Get(col, row)];
                    if (deck != null && deck[row * Size * Size + col] == 0)
                    {
                        val = ' ';
                    }                    
                    
                    s += val.ToString().PadLeft(pad) + " " + ((col + 1) % Size == 0 ? "| " : "");
                }
                s += "\n" + ((row + 1) % Size == 0 ? hline + "\n" : "");
            }
            return s;
        }
    }
}
