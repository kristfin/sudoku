using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Xml.Linq;
using CommandLine;
using Info.Obak.Sudoku.Renderers;

namespace Info.Obak.Sudoku
{
    class Program
    {
        public enum OutputMode
        {
            Text,
            Html
        }
        public class Options
        {
            [Option('o', "output", Default=OutputMode.Text, Required = false, HelpText = "Outputmode for generated sudokus. Text or Html supported.")]
            public OutputMode OutputMode { get; set; }

            [Option('f', "file", Required = false, HelpText = "Output file.  If none, stdout is used.")]
            public string OutputFile { get; set; }

            [Option('c', "count", Default=(uint)1, Required = false, HelpText = "How many sudokus to generate.")]
            public uint Count { get; set; }

            [Option("size", Default=(uint)3, Required = false, HelpText = "How big should the sudokos be.  Only 3, 4 and 5 is supported.")]
            public uint Size { get; set; }

            [Option("seed", Default=(uint)0, Required = false, HelpText = "Initial seed for the sudoku.")]
            public UInt32 Seed { get; set; }

            [Option('l', "level", Default = DifficulityLevel.Medium, Required = false, HelpText = "How hard should the sudokus be. Easy, Medium and Hard is supported")]
            public DifficulityLevel Level { get; set; } = DifficulityLevel.Medium;
        }

        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args).WithParsed(o =>
            {
                if (o.Size < 2 || o.Size > 5)
                {
                    Console.Error.WriteLine("Size must be greater than 1 and less than 6");
                    return;
                }
                UInt32 seed = o.Seed == 0 ? (UInt32)DateTime.UtcNow.Ticks : o.Seed;
                var sudokus = new List<Sudoku>();
                for (int i=0; i<o.Count; i++)
                {
                    var sudoko = new Sudoku((int)o.Size, seed++);
                    sudoko.Solve();
                    sudokus.Add(sudoko);
                }
                var txt = "";
                if (o.OutputMode == OutputMode.Html)
                {
                    txt = new HtmlRenderer().Render(sudokus, o.Level);
                }
                else
                {
                    txt = new TxtRenderer().Render(sudokus, o.Level);
                }
                if (o.OutputFile != null)
                {
                    try
                    {
                        File.WriteAllText(o.OutputFile, txt);
                    }
                    catch(Exception ex)
                    {
                        Console.Error.WriteLine("Unable to write to file '" + o.OutputFile + "': " + ex.Message);
                        return;
                    }
                }
                else
                {
                    Console.WriteLine(txt);
                }
            });
        }
    }
}
