using System.Collections.Generic;

namespace Sudoku.Algorithms
{
    public interface INextEmptyCellFinderAlgoritm
    {

        Cell NextEmptyCell(Board board, List<Cell> excludedCells);
    }
}
