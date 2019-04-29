using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Info.Obak.Sudoku.Renderers
{

    public class TxtRenderer : BaseRenderer, IRenderer
    {
        public string Render(IEnumerable<Sudoku> sudokus, DifficulityLevel level)
        {
            var sb = new StringBuilder();
            var del = "";
            foreach (var sudoku in sudokus)
            {
                (var deck, var given) = GetDeck(sudoku, level);
                sb.AppendLine(del + sudoku.Board.Render(deck));
                sb.AppendLine(level.ToString() + " #" + sudoku.Seed
                    + " " + given + "/" + sudoku.CellCount);
                del = "\n";
            }
            return sb.ToString();
        }
    }
}
