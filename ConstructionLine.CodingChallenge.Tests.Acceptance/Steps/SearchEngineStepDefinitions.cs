namespace ConstructionLine.CodingChallenge.Tests.Acceptance.Steps
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FluentAssertions;

    using TechTalk.SpecFlow;
    using TechTalk.SpecFlow.Assist;

    [Binding]
    public class SearchEngineSteps
    {
        private readonly SearchEngineContext _context;

        public SearchEngineSteps(SearchEngineContext context)
        {
            _context = context;
        }

        [Given(@"I configure search engine with the following details:")]
        public void GivenIConfigureSearchEngineWithTheFollowingDetails(Table table)
        {
            foreach (var data in table.CreateSet<SearchEngineTestData>())
            {
                _context.Shirts.Add(new Shirt(data.Id, data.Name, CreateSize(data.Size), CreateColor(data.Color)));
            }
        }

        [When(@"I do search with the following search options:")]
        public void WhenIDoSearchWithFollowingSearchOptions(Table table)
        {
            var searchOptions = new SearchOptions();

            foreach (var data in table.CreateSet<SearchOptionsTestData>())
            {
                searchOptions.Sizes.Add(CreateSize(data.Size));
                searchOptions.Colors.Add(CreateColor(data.Color));
            }

            _context.SearchOptions = searchOptions;
        }

        [Then(@"I get the following search results:")]
        public async Task ThenIGetFollowingSearchResults(Table table)
        {
            var expectedSearchResults = new SearchResults
                {
                    Shirts = new List<Shirt>(),
                    ColorCounts = new List<ColorCount>(),
                    SizeCounts = new List<SizeCount>()
                };

            foreach (var data in table.CreateSet<SearchEngineTestData>())
            {
                expectedSearchResults.Shirts.Add(new Shirt(data.Id, data.Name, CreateSize(data.Size), CreateColor(data.Color)));
            }

            var searchEngine = new SearchEngine(_context.Shirts);
            var actualResults = await searchEngine.SearchAsync(_context.SearchOptions);

            foreach (var expectedShirt in expectedSearchResults.Shirts)
            {
                var match = actualResults.Shirts.Single(m => m.Id == expectedShirt.Id);
                match.Name.Should().Be(expectedShirt.Name);
            }

            _context.SearchResults = actualResults;
        }

        [Then(@"I get the following size counts:")]
        public void ThenIGetFollowingSizeCounts(Table table)
        {
            foreach (var expectedSizeCount in table.CreateSet<ExpectedSizeCounts>())
            {
                var match = _context.SearchResults.SizeCounts.Single(m => m.Size.Name == expectedSizeCount.Name);
                match.Count.Should().Be(expectedSizeCount.Count);
            }
        }

        [Then(@"I get the following color counts:")]
        public void ThenIGetFollowingColorCounts(Table table)
        {
            foreach (var expectedColorCount in table.CreateSet<ExpectedColorCounts>())
            {
                var match = _context.SearchResults.ColorCounts.Single(m => m.Color.Name == expectedColorCount.Name);
                match.Count.Should().Be(expectedColorCount.Count);
            }
        }

        private static Size CreateSize(string size)
        {
            switch (size.ToLower())
            {
                case "small":
                    return Size.Small;

                case "medium":
                    return Size.Medium;

                case "large":
                    return Size.Large;

                default:
                    return Size.Small;
            }
        }

        private static Color CreateColor(string color)
        {
            switch (color.ToLower())
            {
                case "red":
                    return Color.Red;

                case "blue":
                    return Color.Blue;

                case "black":
                    return Color.Black;

                case "white":
                    return Color.White;

                case "yellow":
                    return Color.Yellow;

                default:
                    return Color.Blue;
            }
        }
    }
}