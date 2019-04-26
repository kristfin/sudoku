using System.Collections.Generic;

namespace Sudoku.Algorithms
{
    public class BruteForceSolverAlgorithm : ISolverAlgorithm
    {
        public bool Solve(Sudoku sudoku)
        {            
            bool stillHope = true;
            while (stillHope)
            {
                stillHope = false;
                if (sudoku.EmptyCount == 0)
                {
                    //  Console.WriteLine("Congrats.  You have a new Sudoku");
                    return true;
                }
                var badCells = new List<Cell>();
                for (int x = 0; x < sudoku.CellCount; x++)
                {
                    var cell = sudoku.NextEmptyCell(badCells);
                    if (cell == null)
                    {
                        return false;
                    }
                    bool goodCell = false;
                    for (int v = 1; v <= sudoku.Size * sudoku.Size; v++)
                    {
                        cell.Value = v;
                        if (sudoku.IsValid(cell))
                        {
                            stillHope = true;
                            goodCell = true;
                            sudoku.SetCell(cell);
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
