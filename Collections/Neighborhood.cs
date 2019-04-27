using System.Collections.Generic;

namespace Info.Obak.Sudoku.Collections
{
    public class Neighborhood : Collection
    {
        public Neighborhood(Location location, List<int> values) : base(CollectionType.Neighborhood, location, values)
        { }
    }
}
