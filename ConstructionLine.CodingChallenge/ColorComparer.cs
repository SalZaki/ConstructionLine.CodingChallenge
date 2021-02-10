namespace ConstructionLine.CodingChallenge
{
    using System.Collections.Generic;

    public class ColorComparer : IEqualityComparer<Color>, IColorComparer
    {
        public bool Equals(Color x, Color y)
        {
            if (ReferenceEquals(x, y))
            {
                return true;
            }

            if (ReferenceEquals(x, null) || ReferenceEquals(y, null))
            {
                return false;
            }

            return x.Id == y.Id && x.Name == y.Name;
        }

        public int GetHashCode(Color color)
        {
            var hashProductName = color.Name == null ? 0 : color.Name.GetHashCode();

            var hashProductCode = color.Id.GetHashCode();

            return hashProductName ^ hashProductCode;
        }
    }
}