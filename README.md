# sudoku

A c# library to create NxN [Sudokos](https://en.wikipedia.org/wiki/Sudoku/)

## Installation

Clone and add reference to your project

## Usage

```c#
using System;
using System.Diagnostics;

namespace MySudoku
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
```

![output](doc/output1.png?raw=true "Output")

## Author
Kristján Þór Finnsson <kristfin@gmail.com>


## License
[MIT](https://choosealicense.com/licenses/mit/)
