using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku.Collections
{
    public class Row : Collection
    {
        public Row(Location location, List<int> values) : base(CollectionType.Row, location, values)
        { }
    }
}
