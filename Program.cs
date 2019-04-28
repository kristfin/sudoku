using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Xml.Linq;

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

            //var file = Path.GetTempFileName() + ".html";
            var file = "/tmp/sudoku.html";
            var list = new List<XElement>();
            for (int i=0; i<9; i++)
            {
                list.Add(Renderer.ToHtmlPart(sudoku, DifficulityLevel.Easy));
                list.Add(Renderer.ToHtmlPart(sudoku, DifficulityLevel.Medium));
                list.Add(Renderer.ToHtmlPart(sudoku, DifficulityLevel.Hard));
            }
            File.WriteAllText(file, Renderer.ToHtml(list.ToArray()));

            Console.WriteLine("Wrote to " + file);
        }
    }
}
