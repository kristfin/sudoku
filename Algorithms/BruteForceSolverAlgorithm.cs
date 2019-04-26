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
                    var cell = NextEmptyCell(sudoku, badCells);
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

        private Cell NextEmptyCell(Sudoku sudoku, List<Cell> excludedCells)
        {
            int size2 = sudoku.Board.Size * sudoku.Board.Size;
            var x = sudoku.Random.Next(0, size2);
            var y = sudoku.Random.Next(0, size2);

            for (int i = 0; i < size2; i++)
            {
                for (int j = 0; j < size2; j++)
                {
                    var cell = new Cell(x, y, sudoku.Board.Get(x, y));
                    if (cell.IsEmpty && !excludedCells.Contains(cell))
                    {
                        return cell;
                    }
                    y = (y + 1) % size2;
                }
                x = (x + 1) % size2;
            }
            return null;
        }
    }
}
