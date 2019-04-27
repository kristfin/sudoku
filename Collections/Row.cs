using System.Collections.Generic;

namespace Info.Obak.Sudoku.Collections
{
    public class Row : Collection
    {
        public Row(Location location, List<int> values) : base(CollectionType.Row, location, values)
        { }
    }
}
