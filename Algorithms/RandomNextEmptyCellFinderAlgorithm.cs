using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku.Algorithms
{
    public class RandomNextEmptyCellFinderAlgorithm : INextEmptyCellFinderAlgoritm
    {
        int Seed;
        Random random;
        public RandomNextEmptyCellFinderAlgorithm(int seed = Int32.MinValue)
        {
            Seed = seed == Int32.MinValue ? (int)DateTime.UtcNow.Ticks : seed;
            random = new Random(Seed);
        }

        public Cell NextEmptyCell(Board board, List<Cell> excludedCells)
        {
            int size = board.Size;
            var x = random.Next(0, size * size);
            var y = random.Next(0, size * size);

            for (int i = 0; i < size * size; i++)
            {
                for (int j = 0; j < size * size; j++)
                {
                    var cell = new Cell(x, y, board.Get(x, y));
                    if (cell.IsEmpty && !excludedCells.Contains(cell))
                    {
                        return cell;
                    }
                    y = (y + 1) % (size * size);
                }
                x = (x + 1) % (size * size);
            }
            return null;
        }
    }
}
