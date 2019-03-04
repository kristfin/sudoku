using System.Collections.Generic;

namespace Sudoku.Collections
{
    public enum CollectionType
    {
        Column,
        Row,
        Neighborhood
    }

    public class Collection
    {
        public List<int> Values { get; }
        public CollectionType Type { get; }
        public int Size { get; }
        public Location Location { get; }

        public Collection(CollectionType type, Location location, List<int> values)
        {
            this.Location = location;
            this.Type = type;
            this.Values = values;
        }

        public virtual bool IsValid(int value)
        {
            return !Values.Contains(value);
        }

        public override string ToString()
        {
            return Type.ToString() + "("+Location+"):" + string.Join(",", Values);
        }
    }
}
