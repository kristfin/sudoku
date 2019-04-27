using System.Collections.Generic;

namespace Info.Obak.Sudoku.Collections
{
    public class Column : Collection
    {
        public Column(Location location, List<int> values) : base(CollectionType.Column, location, values)
        { }        
    }
}
