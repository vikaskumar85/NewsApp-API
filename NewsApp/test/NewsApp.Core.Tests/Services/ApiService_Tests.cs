using Microsoft.Extensions.Configuration;
using Moq;
using NewsApp.Core.Services;
using Xunit;
using Assert = Xunit.Assert;

namespace NewsApp.Core.Tests.Services
{
    public class ApiService_Tests
    {
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly ApiService _apiService;

        public ApiService_Tests()
        {
            _mockConfiguration = new Mock<IConfiguration>();
            _apiService = new ApiService(_mockConfiguration.Object);
        }

        [Fact]
        public async Task GetTopStoryIds_ShouldReturnTopStoryIds_WhenApiReturnsSuccess()
        {
            // Arrange
            var topStoriesUrl = "https://hacker-news.firebaseio.com/v0/topstories.json?print=pretty";
            
            _mockConfiguration.Setup(config => config["TopStoriesUrl"]).Returns(topStoriesUrl);

            // Act
            var result = await _apiService.GetTopStoryIds();

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task GetStoryItem_ShouldReturnNewsItem_WhenApiReturnsSuccess()
        {
            // Arrange
            var storyId = 1;
            var getStoryItemUrl = $"https://hacker-news.firebaseio.com/v0/item/{storyId}.json";
            
            _mockConfiguration.Setup(config => config["GetStoryItemUrl"]).Returns(getStoryItemUrl);

            // Act
            var result = await _apiService.GetStoryItem(storyId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(storyId, result?.Id);
        }
    }
}