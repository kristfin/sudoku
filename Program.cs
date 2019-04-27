using System;
using System.Diagnostics;

namespace Info.Obak.Sudoku
{
    class Program
    {
        static void Main(string[] args)
        {
            int size = args.Length > 0 ? Int32.Parse(args[0]) : 3;
            Stopwatch stopwatch = new Stopwatch();
            var sudoku = new Sudoku(size);
            stopwatch.Restart();
            sudoku.Solve();
            stopwatch.Stop();
            var totalTime = stopwatch.Elapsed.TotalMilliseconds;
            Console.WriteLine("\n" + sudoku);
            Console.WriteLine("Time: " + totalTime.ToString("F2") +"ms");
        }
    }
}
