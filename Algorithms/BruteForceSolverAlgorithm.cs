using System.Collections.Generic;

namespace Sudoku.Algorithms
{
    public class BruteForceSolverAlgorithm : ISolverAlgorithm
    {
        public bool Solve(Sudoku sudoku)
        {
            //private int size2 = sudoku.
            int size2 = sudoku.Size * sudoku.Size;
            int size4 = size2 * size2;
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
                for (int x = 0; x < size4; x++)
                {
                    var cell = sudoku.NextEmptyCell(badCells);
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
                        if (sudoku.IsValid(cell))
                        {
                            stillHope = true;
                            goodCell = true;
                            sudoku.SetCell(cell);
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
