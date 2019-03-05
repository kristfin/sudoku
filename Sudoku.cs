using Sudoku.Collections;
using System;
using System.Collections.Generic;

namespace Sudoku
{    
    public class Sudoku
    {
        Board board;
        Random random;

        private int size2 => board.Size * board.Size;
        private int size4 => board.Size * board.Size * board.Size * board.Size;
        public int SetCount { get; private set; } = 0;
        public int EmptyCount => size4 - SetCount;
        public long IsValidCount { get; private set; } = 0;
        public int Seed { get; }

        public Sudoku(int size=3, int seed = 0)
        {
            this.Seed = seed == 0 ?(int)DateTime.UtcNow.Ticks : seed;
            random = new Random(this.Seed);
            board = new Board(size);
        }

        public override string ToString()
        {
            string s = board.ToString();
            s += "Seed:" + this.Seed + "\n";
            s += "Set cells:" + this.SetCount + "\n";
            s += "Empty cells:" + this.EmptyCount + "\n";
            s += "IsValid count:" + this.IsValidCount;
            return s;
        }        

        public void SetCell(Cell cell)
        {
            SetCount++;
            board.Set(cell.Column, cell.Row, cell.Value);
        }        

        public Cell GetCell(int col, int row)
        {
            return new Cell(col, row, board.Get(col, row));
        }

        public virtual Cell NextEmptyCell(List<Cell> excludedCells)
        {
            int size = board.Size;
            var x = random.Next(0, size*size);
            var y = random.Next(0, size*size);

            for (int i = 0; i < size * size; i++)
            {
                for (int j = 0; j < size * size; j++)
                {
                    var cell = GetCell(x, y);
                    if (cell.IsEmpty && !excludedCells.Contains(cell))
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
                if (cell.Column < ((k + 1) * board.Size))
                {
                    break;
                }
            }
            for (int k = 0; k < board.Size; k++)
            {
                y1 = k * board.Size;
                if (cell.Row < ((k + 1) * board.Size))
                {
                    break;
                }
            }
            var tmp = new List<int>();
            for (int i = x1; i < (x1 + board.Size); i++)
            {
                for (int j = y1; j < (y1 + board.Size); j++)
                {
                    if (!(i == cell.Column && j == cell.Row))
                    {
                        tmp.Add(board.Get(i, j));
                    }                    
                }
            }
            var n = new Neighborhood(cell, tmp);
          //  Console.WriteLine(n.ToString());
            return n;
        }

        public Row GetRow(Location location)
        {
            var tmp = new List<int>();
            for (int i = 0; i < board.Size * board.Size; i++)
            {
                tmp.Add(board.Get(i, location.Row));
            }            
            var r = new Row(location, tmp);
          //  Console.WriteLine(r.ToString());
            return r;
        }
        public Column GetColumn(Location location)
        {
            var tmp = new List<int>();
            for (int i = 0; i < board.Size * board.Size; i++)
            {
                tmp.Add(board.Get(location.Column, i));
            }
            var c = new Column(location, tmp);
          //  Console.WriteLine(c.ToString());
            return c;
        }

        public virtual bool Solve()
        {
            bool stillHope = true;
            while (stillHope)
            {
                stillHope = false;
                if (EmptyCount == 0)
                {
                  //  Console.WriteLine("Congrats.  You have a new Sudoku");
                    return true;
                }
                var badCells = new List<Cell>();
                for (int x = 0; x < size4; x++)
                {
                    var cell = NextEmptyCell(badCells);
                    if (cell == null)
                    {
                       // Console.WriteLine("No more locations");
                        return false;
                    }
                   // Console.WriteLine("Next empty cell:" + cell);
                    bool goodCell = false;
                    for (int v = 1; v <= size2; v++)
                    {
                        cell.Value = v;
                        if (IsValid(cell))
                        {
                            stillHope = true;
                            goodCell = true;
                            SetCell(cell);
                            //Console.WriteLine(sudoku);
                            break;
                        }
                    }
                    if (!goodCell)
                    {
                        badCells.Add(cell);
                    }
                    if (!stillHope)
                    {
                        break;
                    }
                }               
            }
            return false;
        }

    }
}
