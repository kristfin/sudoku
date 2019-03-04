using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku.Collections
{
    public class Neighborhood : Collection
    {
        public Neighborhood(Location location, List<int> values) : base(CollectionType.Neighborhood, location, values)
        { }
    }
}
