using Sudoku.Collections;
using System;
using System.Collections.Generic;

namespace Sudoku
{    
    public class Sudoku
    {
        Board board;
        Random random;

        public int SetCount { get; private set; } = 0;
        public int EmptyCount => board.Size * board.Size * board.Size * board.Size - SetCount;
        public long IsValidCount { get; private set; } = 0;

        public Sudoku(int size=3, int seed = 0)
        {            
            random = seed != 0 ? new Random(seed) : new Random();
            board = new Board(size);
        }

        public override string ToString()
        {
            string s = board.ToString();
            s += "Set cells " + this.SetCount + "\n";
            s += "Empty cells " + this.EmptyCount + "\n";
            s += "IsValid count " + this.IsValidCount;
            return s;
        }        

        public void SetCell(Cell cell)
        {
            SetCount++;
            board.Set(cell.X, cell.Y, cell.Value);
        }        

        public Cell GetCell(int x, int y)
        {
            return new Cell(x, y, board.Get(x, y));
        }

        public virtual Cell NextEmptyCell()
        {
            int size = board.Size;
            var x = random.Next(0, size*size);
            var y = random.Next(0, size*size);

            for (int i = 0; i < size * size; i++)
            {
                for (int j = 0; j < size * size; j++)
                {
                    var cell = GetCell(x, y);
                    if (cell.IsEmpty)
                    {
                        return cell;
                    }
                    else
                    {
                       // Console.WriteLine(cell + " is filled");
                    }                        
                    y = (y + 1) % (size * size);
                }
                x = (x + 1) % (size * size);
            }
            return null;
        }

        public bool IsValid(Cell cell)
        {
            IsValidCount++;
            return cell != null
                && GetRow(cell).IsValid(cell.Value)
                && GetColumn(cell).IsValid(cell.Value)
                && GetNeighborhood(cell).IsValid(cell.Value);
        }
                
        
        public Neighborhood GetNeighborhood(Cell cell)
        {
            var x1 = 0; // x < 3 ? 0 : x < 6 ? 3 : 6;
            var y1 = 0; // y < 3 ? 0 : y < 6 ? 3 : 6;            
            for (int k = 0; k < board.Size*board.Size; k++)
            {
                x1 = k * board.Size;
                if (cell.X < ((k + 1) * board.Size))
                {
                    break;
                }
            }
            for (int k = 0; k < board.Size; k++)
            {
                y1 = k * board.Size;
                if (cell.Y < ((k + 1) * board.Size))
                {
                    break;
                }
            }
            var tmp = new List<int>();
            for (int i = x1; i < (x1 + board.Size); i++)
            {
                for (int j = y1; j < (y1 + board.Size); j++)
                {
                    if (!(i == cell.X && j == cell.Y))
                    {
                        tmp.Add(board.Get(i, j));
                    }                    
                }
            }
            var n = new Neighborhood(cell, tmp);
            Console.WriteLine(n.ToString());
            return n;
        }


        public Row GetRow(Location location)
        {
            var tmp = new List<int>();
            for (int i = 0; i < board.Size * board.Size; i++)
            {
                tmp.Add(board.Get(i, location.Y));
            }            
            var r = new Row(location, tmp);
            Console.WriteLine(r.ToString());
            return r;
        }
        public Column GetColumn(Location location)
        {
            var tmp = new List<int>();
            for (int i = 0; i < board.Size * board.Size; i++)
            {
                tmp.Add(board.Get(location.X, i));
            }
            var c = new Column(location, tmp);
            Console.WriteLine(c.ToString());
            return c;
        }

    }
}
