using Sudoku.Collections;
using System;
using System.Collections.Generic;

namespace Sudoku
{
    public class Sudoku
    {
        internal Board Board { get; }
        internal Random Random { get; }
        public int SetCount { get; private set; } = 0;
        public int EmptyCount => CellCount - SetCount;
        public long IsValidCount { get; private set; } = 0;
        public int Seed { get; }
        public int Size => Board.Size;
        public int CellCount => (int)Math.Pow(Board.Size, 4);        

        public Sudoku(int size=3, int seed = Int32.MinValue)
        {            
            Seed = seed == Int32.MinValue ? (int)DateTime.UtcNow.Ticks : seed;            
            Random = new Random(this.Seed);
            Board = new Board(size);
        }

        private IEnumerable<Cell> GetCells()
        {
            var list = new List<Cell>();
            for(int x =0; x<Size*Size; x++)
            {
                for (int y = 0; y < Size * Size; y++)
                {
                    var cell = GetCell(x, y);
                    if (!cell.IsEmpty)
                    {
                        list.Add(cell);
                    }
                }
            }
            return list;
        }

        public void Reset()
        {
            this.SetCount = 0;
            this.IsValidCount = 0;
            this.Board.Reset();
        }

        public override string ToString()
        {
            string s = Board.ToString();
            s += "Seed:" + this.Seed + "\n";
            s += "IsValid count:" + this.IsValidCount;
            return s;
        }        

        public void SetCell(Cell cell)
        {
            SetCount++;
            Board.Set(cell.Column, cell.Row, cell.Value);
        }

        public void ClearCell(Cell cell)
        {
            SetCount--;
            Board.Set(cell.Column, cell.Row, 0);
        }

        public void SetCells(List<Cell> history)
        {
            foreach(var cell in history)
            {
                SetCell(cell);
            }            
        }

        public Cell GetCell(int col, int row)
        {
            return new Cell(col, row, Board.Get(col, row));
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
            var x1 = 0; 
            var y1 = 0; 
            for (int k = 0; k < Board.Size*Board.Size; k++)
            {
                x1 = k * Board.Size;
                if (cell.Column < ((k + 1) * Board.Size))
                {
                    break;
                }
            }
            for (int k = 0; k < Board.Size; k++)
            {
                y1 = k * Board.Size;
                if (cell.Row < ((k + 1) * Board.Size))
                {
                    break;
                }
            }
            var tmp = new List<int>();
            for (int i = x1; i < (x1 + Board.Size); i++)
            {
                for (int j = y1; j < (y1 + Board.Size); j++)
                {
                    if (!(i == cell.Column && j == cell.Row))
                    {
                        tmp.Add(Board.Get(i, j));
                    }                    
                }
            }
            var n = new Neighborhood(cell, tmp);
            return n;
        }

        public Row GetRow(Location location)
        {
            var tmp = new List<int>();
            for (int i = 0; i < Board.Size * Board.Size; i++)
            {
                tmp.Add(Board.Get(i, location.Row));
            }            
            var r = new Row(location, tmp);
            return r;
        }

        public Column GetColumn(Location location)
        {
            var tmp = new List<int>();
            for (int i = 0; i < Board.Size * Board.Size; i++)
            {
                tmp.Add(Board.Get(location.Column, i));
            }
            var c = new Column(location, tmp);
            return c;
        }

        public virtual bool Solve()
        {
            if (EmptyCount == 0)
            {
                return true;
            }

            Cell cell = null;
            int size2 = Size * Size;

            for (int y = 0; y < size2; y++)
            {
                for (int x = 0; x < size2; x++)
                {
                    var tmp = GetCell(x, y);
                    if (tmp.IsEmpty)
                    {
                        cell = tmp;
                        break;
                    }
                }
            }

            if (cell == null)
            {
                return false;
            }

            var v = Random.Next(0, size2);

            for (int i = 0; i < size2; i++)
            {
                cell.Value = (v++ % size2) + 1;
                if (IsValid(cell))
                {
                    SetCell(cell);
                    if (Solve())
                    {
                        return true;
                    }
                    ClearCell(cell);
                }
            }
            return false;
        }       
    }
}
