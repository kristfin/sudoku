using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku.Algorithms
{
    public class SimpleNextEmptyCellFinderAlgorithm : INextEmptyCellFinderAlgoritm
    {
        public Cell NextEmptyCell(Board board, List<Cell> excludedCells)
        {
            int size = board.Size;
     
            for (int x = 0; x < size * size; x++)
            {
                for (int y = 0; y < size * size; y++)
                {
                    var cell = new Cell(x, y, board.Get(x, y));
                    if (cell.IsEmpty && !excludedCells.Contains(cell))
                    {
                        return cell;
                    }
                }
            }
            return null;
        }
    }
}
