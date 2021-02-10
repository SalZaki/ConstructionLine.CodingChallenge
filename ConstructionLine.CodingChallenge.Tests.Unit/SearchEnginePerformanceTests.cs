namespace ConstructionLine.CodingChallenge.Tests.Unit
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading.Tasks;

    using ConstructionLine.CodingChallenge.Tests.Unit.SampleData;

    using NUnit.Framework;

    [TestFixture]
    public class SearchEnginePerformanceTests : SearchEngineTestsBase
    {
        private List<Shirt> _shirts;
        private SearchEngine _searchEngine;
        private Stopwatch _sw;

        [SetUp]
        public void Setup()
        {
            var dataBuilder = new SampleDataBuilder(50000);
            _shirts = dataBuilder.CreateShirts();
            _searchEngine = new SearchEngine(_shirts);
            _sw = new Stopwatch();
        }

        [Category("Performance")]
        [Test(Author = "Sal Zaki", TestOf = typeof(SearchEngine), Description = "GIVEN a search engine with sample data-set AND search options, and WHEN search is called, THEN the call should be executed in less than 100 milliseconds.")]
        [TestCaseSource(nameof(SearchOptionsTestCases))]
        [Timeout(5000)]
        public async Task PerformanceTests(SearchOptions searchOptions, long durationInMilliseconds)
        {
            _sw?.Start();

            var results = await _searchEngine.SearchAsync(searchOptions);

            _sw?.Stop();

            Assert.IsTrue(_sw.ElapsedMilliseconds < durationInMilliseconds);
            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(_shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(_shirts, searchOptions, results.ColorCounts);
        }

        private static List<TestCaseData> SearchOptionsTestCases() => new List<TestCaseData>
            {
                new TestCaseData(new SearchOptions { Sizes = new List<Size> { Size.Small } }, 100)
                    .SetName("Search shirts with 'Small' size"),

                new TestCaseData(new SearchOptions { Sizes = new List<Size> { Size.Small, Size.Medium } }, 100)
                    .SetName("Search shirts with 'Small', 'Medium' sizes"),

                new TestCaseData(new SearchOptions { Sizes = new List<Size> { Size.Small, Size.Medium, Size.Large } }, 100)
                    .SetName("Search shirts with 'Small', 'Medium', 'Large' sizes"),

                new TestCaseData(new SearchOptions { Sizes = new List<Size> { Size.Small }, Colors = new List<Color> { Color.White } }, 100)
                    .SetName("Search shirts with 'Small' size in 'White' color"),

                new TestCaseData(new SearchOptions { Sizes = new List<Size> { Size.Small }, Colors = new List<Color> { Color.White, Color.Black } }, 100)
                    .SetName("Search shirts with 'Small' size in 'White', 'Black' colors"),

                new TestCaseData(new SearchOptions { Sizes = new List<Size> { Size.Small }, Colors = new List<Color> { Color.White, Color.Black, Color.Blue } }, 100)
                    .SetName("Search shirts with 'Small' size in 'White', 'Black', 'Blue' colors"),

                new TestCaseData(new SearchOptions { Sizes = new List<Size> { Size.Small }, Colors = new List<Color> { Color.White, Color.Black, Color.Blue, Color.Red } }, 100)
                    .SetName("Search shirts with 'Small' size in 'White', 'Black', 'Blue', 'Red' colors"),

                new TestCaseData(new SearchOptions { Sizes = new List<Size> { Size.Small }, Colors = Color.All }, 100)
                    .SetName("Search shirts with 'Small' size in all colors"),

                new TestCaseData(new SearchOptions { Sizes = new List<Size> { Size.Small, Size.Medium }, Colors = new List<Color> { Color.White } }, 100)
                    .SetName("Search shirts with 'Small', 'Medium' sizes in 'White' color"),

                new TestCaseData(new SearchOptions { Sizes = new List<Size> { Size.Small, Size.Medium }, Colors = new List<Color> { Color.White, Color.Black } }, 100)
                    .SetName("Search shirts with 'Small', 'Medium' sizes in 'White', 'Black' colors"),

                new TestCaseData(new SearchOptions { Sizes = new List<Size> { Size.Small, Size.Medium }, Colors = new List<Color> { Color.White, Color.Black, Color.Blue } }, 100)
                    .SetName("Search shirts with 'Small', 'Medium' sizes in 'White', 'Black', 'Blue' colors"),

                new TestCaseData(new SearchOptions { Sizes = new List<Size> { Size.Small, Size.Medium }, Colors = new List<Color> { Color.White, Color.Black, Color.Blue } }, 100)
                    .SetName("Search shirts with 'Small', 'Medium' sizes in 'White', 'Black', 'Blue' colors"),

                new TestCaseData(new SearchOptions { Sizes = new List<Size> { Size.Small, Size.Medium }, Colors = new List<Color> { Color.White, Color.Black, Color.Blue, Color.Red } }, 100)
                    .SetName("Search shirts with 'Small', 'Medium' sizes in 'White', 'Black', 'Blue', 'Red' colors"),

                new TestCaseData(new SearchOptions { Sizes = new List<Size> { Size.Small, Size.Medium }, Colors = Color.All }, 100)
                    .SetName("Search shirts with 'Small', 'Medium' sizes in all colors"),
            };
    }
}