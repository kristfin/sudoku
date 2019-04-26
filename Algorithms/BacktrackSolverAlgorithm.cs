using System;
using System.Collections.Generic;

namespace Sudoku.Algorithms
{
    public class BacktrackSolverAlgorithm : ISolverAlgorithm
    {
        public bool Solve(Sudoku sudoku)
        {
            var history = new List<Cell>();
            int max = 500;
            int idx = 0;

            while (max-- > 0)
            {
                idx = history.Count;

                if (Solve(sudoku, history))
                {
                    return true;
                }
            }
            return false;
        }

        private bool Solve(Sudoku sudoku, List<Cell> history)
        {
            sudoku.Reset();
            sudoku.SetCells(history);

            if (sudoku.EmptyCount == 0)
            {
                return true;
            }

            Cell cell = null;
            int size2 = sudoku.Size * sudoku.Size;

            for (int y = 0; y < size2; y++)
            {
                for (int x = 0; x < size2; x++)
                {
                    var tmp = sudoku.GetCell(x, y);
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
                       

            var v = (int)(DateTime.UtcNow.Ticks % (size2));

            for (int i = 0; i < size2; i++)
            {
                cell.Value = (v++ % size2) + 1;
                if (sudoku.IsValid(cell))
                {
               //     sudoku.SetCell(cell);
                //    Console.WriteLine(sudoku);                    
                    history.Add(cell);
                    if (Solve(sudoku, history))
                    {
                        return true;
                    }
                    history.RemoveAt(history.Count - 1);
                }
            }
            return false;
        }      
    }
}
