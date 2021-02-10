namespace ConstructionLine.CodingChallenge
{
    public interface IColorComparer
    {
        bool Equals(Color x, Color y);

        int GetHashCode(Color obj);
    }
}