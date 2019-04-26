using Sudoku.Algorithms;
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
        public int EmptyCount => CellCount - SetCount;
        public long IsValidCount { get; private set; } = 0;
        public int Seed { get; }
        public int Size => board.Size;
        public int CellCount => (int)Math.Pow(board.Size, 4);

        public INextEmptyCellFinderAlgoritm NextEmptyCellFinderAlgoritm {get; set;}
        
        public ISolverAlgorithm SolverAlgorithm { get; set; }
        public IEnumerable<Cell> Cells => GetCells();

        public Sudoku(int size=3, int seed = Int32.MinValue)
        {            
            this.Seed = seed == Int32.MinValue ? (int)DateTime.UtcNow.Ticks : seed;
            NextEmptyCellFinderAlgoritm = new RandomNextEmptyCellFinderAlgorithm(seed);
            SolverAlgorithm = new BruteForceSolverAlgorithm();
            random = new Random(this.Seed);
            board = new Board(size);
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
            this.board.Reset();
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

        public void SetCells(List<Cell> history)
        {
            foreach(var cell in history)
            {
                SetCell(cell);
            }            
        }

        public Cell GetCell(int col, int row)
        {
            return new Cell(col, row, board.Get(col, row));
        }

        public virtual Cell NextEmptyCell(List<Cell> excludedCells)
            => NextEmptyCellFinderAlgoritm.NextEmptyCell(board, excludedCells);

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
            => SolverAlgorithm.Solve(this);        

    }
}
