using System;
using System.Collections.Generic;
using System.IO;

namespace Sudoku
{
    class Program
    {
        static void Main(string[] args)
        {
            int size = 3;
            var file = File.AppendText(@"c:\tmp\sudoku.txt");
            file.AutoFlush = true;
            while (true)
            {
                var sudoku = new Sudoku(size);//, 262632719);//, 2041746577);
                var b = sudoku.Solve();
                if (b==true)
                {
                    Console.WriteLine("\n" + sudoku);
                    file.WriteLine(sudoku.Seed);                    
                }
                else if (sudoku.EmptyCount < 3)                
                {
                    Console.WriteLine("----------");
                    Console.WriteLine("Seed:" + sudoku.Seed);
                    Console.WriteLine("EmptyCount:" + sudoku.EmptyCount);
                }          
                else
                {
                    Console.Write(".");
                }
            }
        }
    }
}
