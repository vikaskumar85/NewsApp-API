using Microsoft.Extensions.Caching.Memory;
using Moq;
using NewsApp.Core.Enums;
using NewsApp.Core.Services;
using Xunit;
using Assert = Xunit.Assert;

namespace NewsApp.Core.Tests.Services
{
    public class CacheService_Tests
    {
        private readonly Mock<IMemoryCache> _cacheMock;
        private readonly CacheService _cacheService;

        public CacheService_Tests()
        {
            _cacheMock = new Mock<IMemoryCache>();
            _cacheService = new CacheService(_cacheMock.Object);
        }

        [Fact]
        public async Task RetrieveFromCacheAsync_ReturnsCacheStatusError_WhenExceptionIsThrown()
        {
            // Arrange
            var key = "testKey";
            _cacheMock.Setup(cache => cache.TryGetValue(key, out It.Ref<object>.IsAny)).Throws(new Exception("Cache retrieval error"));

            // Act
            var result = await _cacheService.RetrieveFromCacheAsync(key);

            // Assert
            Assert.Equal(CacheStatusOption.Error, result.CacheStatus);
            Assert.NotNull(result.Error);
            Assert.Equal("Cache retrieval error", result.Error.Message);
        }

        [Fact]
        public async Task SaveToCacheAsync_ReturnsCacheStatusError_WhenExceptionIsThrown()
        {
            // Arrange
            var key = "testKey";
            var objectToCache = "testValue";
            _cacheMock.Setup(cache => cache.TryGetValue(key, out It.Ref<object>.IsAny)).Throws(new Exception("Cache saving error"));

            // Act
            var result = await _cacheService.SaveToCacheAsync(key, objectToCache);

            // Assert
            Assert.Equal(CacheStatusOption.Error, result.CacheStatus);
            Assert.NotNull(result.Error);
            Assert.Equal("Cache saving error", result.Error.Message);
        }
    }
}