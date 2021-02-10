namespace ConstructionLine.CodingChallenge.Tests.Acceptance
{
    using System.Collections.Generic;

    public class SearchEngineContext
    {
        public SearchEngineContext()
        {
            Shirts = new List<Shirt>();
        }

        public List<Shirt> Shirts { get; set; }

        public SearchOptions SearchOptions { get; set; }

        public SearchResults SearchResults { get; set; }
    }
}