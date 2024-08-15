using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NewsApp.API.Controllers;
using NewsApp.Core.Interfaces;
using NewsApp.Core.Models;
using Xunit;
using Assert = Xunit.Assert;

namespace NewsApp.API.Tests.Controllers
{
    public class NewsController_Tests
    {
        private readonly Mock<ILogger<NewsController>> _loggerMock;
        private readonly Mock<INewsService> _newsServiceMock;
        private readonly NewsController _controller;

        public NewsController_Tests()
        {
            _loggerMock = new Mock<ILogger<NewsController>>();
            _newsServiceMock = new Mock<INewsService>();
            _controller = new NewsController(_loggerMock.Object, _newsServiceMock.Object);
        }

        [Fact]
        public async Task GetAll_ShouldReturnOk_WhenDataIsAvailable()
        {
            // Arrange
            var resultModel = new List<NewsItemModel> { new NewsItemModel { Id = 1, Title = "Sample Title", Url = "http://example.com" } };
            
            _newsServiceMock.Setup(s => s.GetAllStories()).ReturnsAsync(resultModel);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(resultModel);
        }

        [Fact]
        public async Task GetAll_ShouldReturnNotFound_WhenNoDataIsAvailable()
        {
            // Setup
            _newsServiceMock.Setup(s => s.GetAllStories()).ReturnsAsync((IEnumerable<NewsItemModel>)null);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result.Result); ;
            notFoundResult.Should().NotBeNull();
            notFoundResult.StatusCode.Should().Be(404);
        }
    }
}