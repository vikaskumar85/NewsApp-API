using AutoFixture;
using Moq;
using NewsApp.Core.Enums;
using NewsApp.Core.Interfaces;
using NewsApp.Core.Models;
using NewsApp.Core.Services;
using Xunit;
using Assert = Xunit.Assert;

namespace NewsApp.Core.Tests.Services
{
    public class NewsService_Tests
    {
        private readonly Mock<ICacheService> _mockCacheService;
        private readonly Mock<IApiService> _mockApiService;
        private readonly NewsService _newsService;
        private readonly IFixture _fixture;

        public NewsService_Tests()
        {
            _fixture = new Fixture();
            _mockCacheService = new Mock<ICacheService>();
            _mockApiService = new Mock<IApiService>();
            _newsService = new NewsService(_mockCacheService.Object, _mockApiService.Object);
        }

        [Fact]
        public async Task GetAllStories_ShouldReturnStoriesFromCache()
        {
            // Arrange
            var cachedStories = new List<NewsItemModel>
        {
            new NewsItemModel { Id = 1, Title = "Cached Title", Url = "CachedUrl" }
        };

            _mockCacheService.Setup(cache => cache.RetrieveFromCacheAsync(It.IsAny<string>())).ReturnsAsync(new CacheResultModel("TopStoriesDataKey")
            {
                CacheStatus = CacheStatusOption.Exists,
                CacheValue = cachedStories
            });

            // Act
            var result = await _newsService.GetAllStories();

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("Cached Title", result.First().Title);
        }

        [Fact]
        public async Task GetAllStories_ShouldCacheNewStories()
        {
            // Arrange
            var storyIds = new List<int> { 1, 2 };
            var storyItems = new List<NewsItemModel>
            {
                new NewsItemModel { Id = 1, Title = "Title 1", Url = "Url1" },
                new NewsItemModel { Id = 2, Title = "Title 2", Url = "Url2" }
            };
            var key = _fixture.Create<string>();
            var objectToCache = _fixture.Create<IEnumerable<NewsItemModel>>();

            _mockApiService.Setup(api => api.GetTopStoryIds()).ReturnsAsync(storyIds);
            _mockApiService.Setup(api => api.GetStoryItem(It.IsAny<int>())).ReturnsAsync((int id) => storyItems.FirstOrDefault(item => item.Id == id));
            _mockCacheService.Setup(cache => cache.RetrieveFromCacheAsync(It.IsAny<string>())).ReturnsAsync(new CacheResultModel("TopStoriesDataKey")
            {
                CacheStatus = CacheStatusOption.DoesNotExists
            });

            // Act
            var result = await _newsService.GetAllStories();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }
    }

}