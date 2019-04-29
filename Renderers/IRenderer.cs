using System.Collections.Generic;

namespace Info.Obak.Sudoku.Renderers
{
    public interface IRenderer
    {
        string Render(IEnumerable<Sudoku> sudokus, DifficulityLevel level);
    }
}
