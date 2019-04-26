using System.Collections.Generic;

namespace Sudoku.Algorithms
{
    public interface ISolverAlgorithm
    {        
        bool Solve(Sudoku sudoku);
    }
}
