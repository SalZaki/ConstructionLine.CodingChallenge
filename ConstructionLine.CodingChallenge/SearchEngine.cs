namespace ConstructionLine.CodingChallenge
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class SearchEngine
    {
        private readonly SemaphoreSlim _throttler;

        private readonly ICollection<Task> _allTasks;

        private readonly ICollection<Shirt> _shirts;

        public SearchEngine(List<Shirt> shirts, int concurrentRequests = 15)
        {
            if (shirts == null)
            {
                throw new ArgumentNullException(nameof(shirts));
            }

            _shirts = new HashSet<Shirt>(shirts);
            _allTasks = new List<Task>(10);
            _throttler = new SemaphoreSlim(concurrentRequests);
        }

        public async Task<SearchResults> SearchAsync(SearchOptions options)
        {
            if (options == null)
            {
                throw new ArgumentException("Search options cannot be null");
            }

            if (options.Colors.Any() == false)
            {
                options.Colors = Color.All;
            }

            if (options.Sizes.Any() == false)
            {
                options.Sizes = Size.All;
            }

            var sizesHash = new HashSet<Size>(options.Sizes);
            var colorsHash = new HashSet<Color>(options.Colors);

            var searchResults = new SearchResults
            {
                Shirts = new List<Shirt>(),
                ColorCounts = new List<ColorCount>(),
                SizeCounts = new List<SizeCount>()
            };

            await _throttler.WaitAsync();
            _allTasks.Add(
                Task.Run(
                    async () =>
                        {
                            try
                            {
                                var results = await GetShirtsByAsync(_shirts, sizesHash, colorsHash);
                                var colorCounts = await GetColorCountsByAsync(Color.All.AsReadOnly(), results.AsReadOnly());
                                var sizeCounts = await GetSizeCountsByAsync(Size.All.AsReadOnly(), results.AsReadOnly());

                                searchResults.Shirts.AddRange(results);
                                searchResults.ColorCounts.AddRange(colorCounts);
                                searchResults.SizeCounts.AddRange(sizeCounts);
                            }
                            finally
                            {
                                _throttler.Release();
                            }
                        }));

            await WhenAllAndThrow(_allTasks);

            return searchResults;
        }

        private static async Task<List<Shirt>> GetShirtsByAsync(
            ICollection<Shirt> shirts,
            HashSet<Size> sizes,
            HashSet<Color> colors)
        {
            return await Task.FromResult(shirts
                       .Where(x => sizes.Contains(x.Size, new SizeComparer())).AsParallel().ToList()
                       .Where(x => colors.Contains(x.Color, new ColorComparer())).AsParallel().ToList());
        }

        private static async Task<List<ColorCount>> GetColorCountsByAsync(
            ICollection<Color> colors,
            IReadOnlyCollection<Shirt> shirts)
        {
            return await Task.FromResult(
                       colors
                           .Select(color => new ColorCount { Color = color, Count = shirts.Count(s => s.Color.Id == color.Id) })
                           .AsParallel()
                           .ToList());
        }

        private static async Task<List<SizeCount>> GetSizeCountsByAsync(
            ICollection<Size> sizes,
            IReadOnlyCollection<Shirt> shirts)
        {
            return await Task.FromResult(
                       sizes
                           .Select(size => new SizeCount { Size = size, Count = shirts.Count(s => s.Size.Id == size.Id) })
                           .AsParallel()
                           .ToList());
        }

        private static async Task WhenAllAndThrow(ICollection<Task> tasks)
        {
            await Task.WhenAll(tasks);
            if (tasks != null)
            {
                foreach (var task in tasks)
                {
                    if (task.Exception != null)
                    {
                        throw task.Exception;
                    }
                }
            }
        }
    }
}