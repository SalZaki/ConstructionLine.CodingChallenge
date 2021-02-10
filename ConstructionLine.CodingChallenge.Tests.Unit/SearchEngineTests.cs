namespace ConstructionLine.CodingChallenge.Tests.Unit
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using NUnit.Framework;

    [TestFixture]
    public class SearchEngineTests : SearchEngineTestsBase
    {
        [Category("Unit")]
        [Test(Author = "Sal Zaki", TestOf = typeof(SearchEngine), Description = "Unit tests for all given search option scenarios.")]
        [TestCaseSource(nameof(SearchOptionsTestCases))]
        [Timeout(5000)]
        public async Task Tests(SearchOptions searchOptions)
        {
            var shirts = new List<Shirt>
                {
                    new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                    new Shirt(Guid.NewGuid(), "Black - Medium", Size.Medium, Color.Black),
                    new Shirt(Guid.NewGuid(), "Blue - Large", Size.Large, Color.Blue),
                };
 
            var searchEngine = new SearchEngine(shirts);

            var results = await searchEngine.SearchAsync(searchOptions);

            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(shirts, searchOptions, results.ColorCounts);
        }

        private static List<TestCaseData> SearchOptionsTestCases() =>
            new List<TestCaseData>
                {
                    new TestCaseData(new SearchOptions())
                        .SetName("Search with no options"),
                    new TestCaseData(new SearchOptions { Sizes = new List<Size> { Size.Small } })
                        .SetName("Search shirts with 'Small' size"),
                    new TestCaseData(new SearchOptions { Sizes = new List<Size> { Size.Small }, Colors = new List<Color> { Color.White } })
                        .SetName("Search shirts with 'Small' size in 'White' color")
                };
    }
}