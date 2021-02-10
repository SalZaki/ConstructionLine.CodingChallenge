namespace ConstructionLine.CodingChallenge
{
    using System;

    public class Shirt
    {
        public Shirt(Guid id, string name, Size size, Color color)
        {
            Id = id;
            Name = name;
            Size = size;
            Color = color;
        }

        public Guid Id { get; set;  }

        public string Name { get; set;  }

        public Size Size { get; set; }

        public Color Color { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}, Color: {Color}, Size: {Size}";
        }
    }
}