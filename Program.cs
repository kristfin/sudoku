using Sudoku.Algorithms;
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
          //  while (true)
            {
                int seed = (int)DateTime.UtcNow.Ticks;
                var sudoku = new Sudoku(size, seed);
                // sudoku.NextEmptyCellFinderAlgoritm = new RandomNextEmptyCellFinderAlgorithm(seed);
                // sudoku.SolverAlgorithm = new BruteForceSolverAlgorithm();
                sudoku.SolverAlgorithm = new BacktrackSolverAlgorithm();
                sudoku.NextEmptyCellFinderAlgoritm = new SimpleNextEmptyCellFinderAlgorithm();
                
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
