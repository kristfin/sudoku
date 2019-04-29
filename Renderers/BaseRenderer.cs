using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Info.Obak.Sudoku.Renderers
{
    public class BaseRenderer
    {
        protected Tuple<int[], int> GetDeck(Sudoku sudoku, DifficulityLevel level)
        {
            var deck = new int[sudoku.CellCount];
            var givens = (int)level * sudoku.CellCount / 81;
            for (int i = 0; i < givens; i++)
            {
                while (true)
                {
                    int idx = sudoku.Random.Next(sudoku.CellCount);
                    if (deck[idx] == 0)
                    {
                        deck[idx] = 1;
                        break;
                    }
                }
            }
            return new Tuple<int[], int>(deck, givens);
        }
    }
}