namespace Presentation.Api.Tests.Services.Storage
{
    using global::Services.Storage;
    using Grpc.Core;
    using Microsoft.Extensions.Logging;
    using Moq;
    using PvStorageService;
    using PvStorageService.Application.Services;
    using Xunit;

    public class StorageServiceTests
    {
        [Fact]
        public async Task StoreData_Success()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<StorageService>>();
            var contentServiceMock = new Mock<IContentFileService>();

            var storageService = new StorageService(loggerMock.Object, contentServiceMock.Object);

            var request = new DataRequest
            {
                UserAgent = "TestUserAgent",
                IpAddress = "127.0.0.1",
                Referer = "TestReferer"
            };

            var serverCallContext = new Mock<ServerCallContext>().Object;

            // Act
            var result = await storageService.StoreData(request, serverCallContext);

            // Assert
            Assert.Equal("Success! Data Save", result.Message);
            contentServiceMock.Verify(mock => mock.SaveContent(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task StoreData_InvalidArgument()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<StorageService>>();
            var contentServiceMock = new Mock<IContentFileService>();

            var storageService = new StorageService(loggerMock.Object, contentServiceMock.Object);

            var request = new DataRequest
            {
                UserAgent = "TestUserAgent",
                IpAddress = "", // Setting IpAddress as empty string to simulate an invalid argument
                Referer = "TestReferer"
            };

            var serverCallContext = new Mock<ServerCallContext>().Object;

            // Act & Assert
            await Assert.ThrowsAsync<RpcException>(() => storageService.StoreData(request, serverCallContext));
            contentServiceMock.Verify(mock => mock.SaveContent(It.IsAny<string>()), Times.Never);
        }
    }
}