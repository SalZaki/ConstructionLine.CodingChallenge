namespace ConstructionLine.CodingChallenge
{
    public interface ISizeComparer
    {
        bool Equals(Size x, Size y);

        int GetHashCode(Size obj);
    }
}
