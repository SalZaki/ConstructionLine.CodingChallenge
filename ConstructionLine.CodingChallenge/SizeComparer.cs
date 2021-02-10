namespace ConstructionLine.CodingChallenge
{
    using System.Collections.Generic;

    public class SizeComparer : IEqualityComparer<Size>, ISizeComparer
    {
        public bool Equals(Size x, Size y)
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

        public int GetHashCode(Size size)
        {
            var hashProductName = size.Name == null ? 0 : size.Name.GetHashCode();

            var hashProductCode = size.Id.GetHashCode();

            return hashProductName ^ hashProductCode;
        }
    }
}