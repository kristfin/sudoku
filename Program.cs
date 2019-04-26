using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            int total = 0;
            int success = 0;
            double totalTime = 0;
            Stopwatch stopwatch = new Stopwatch();
            while (true)
            {                
                var sudoku = new Sudoku(size);//, 262632719);//, 2041746577);
                stopwatch.Restart();
                var b = sudoku.Solve();
                stopwatch.Stop();
                totalTime += stopwatch.Elapsed.TotalMilliseconds;
                total++;
                if (b==true)
                {
                    success++;
                    Console.WriteLine("\n" + sudoku);
                    file.WriteLine(sudoku.Seed);
                    Console.WriteLine(success + "/" + total + " " + (100.0*success / total).ToString("F6"));
                    Console.WriteLine("Time per sudoku try: " + (totalTime / total).ToString("F4"));
                    Console.WriteLine("Time per sudoku: " + (totalTime / success).ToString("F4"));

                }
                /*
                else if (sudoku.EmptyCount < 3)                
                {
                    Console.WriteLine("\n----------");
                    Console.WriteLine("Seed:" + sudoku.Seed);
                    Console.WriteLine("EmptyCount:" + sudoku.EmptyCount);
                }          
                */
                else
                {
                    if (total % 100 == 0)
                    {
                        Console.Write(".");
                    }
                }

            }
        }
    }
}
