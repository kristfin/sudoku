using System;

namespace Sudoku
{
    class Program
    {
        static void Main(string[] args)
        {
            int size = 3;
            // var sudoku = new Sudoku(size, 1234);
            var sudoku = new Sudoku(size);
            Console.WriteLine(sudoku);

            sudoku.SetCell(new Cell(0, 0, 1));
            sudoku.SetCell(new Cell(1, 0, 2));
            Console.WriteLine(sudoku);


            for (int i=0; i<size*size*size*size; i++)
            {
                if (sudoku.EmptyCount == 0)
                {
                    Console.WriteLine("Congrats.  You have a new Sudoku");
                    break;
                }
                var cell = sudoku.NextEmptyCell();
                if (cell == null)
                {
                    Console.WriteLine("No more locations");
                    break;
                }
                for (int v=1; v<=size*size; v++)
                {
                    cell.Value = v;
                    if (sudoku.IsValid(cell))
                    {
                        sudoku.SetCell(cell);
                        Console.WriteLine(sudoku);
                        break;
                    }
                }                    
            }
            Console.WriteLine(sudoku);
        }
    }
}
