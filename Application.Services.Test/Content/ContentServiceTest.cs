namespace Application.Services.Test.Content
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using Moq;
    using PvStorageService.Application.Services;
    using Xunit;

    public class ContentServiceTests
    {
        [Fact]
        public void SaveContent_Success()
        {
            // Arrange
            var configurationMock = new Mock<IConfiguration>();
            configurationMock.Setup(config => config["FilePath"]).Returns("C:/somedirectory/testfile.txt");

            var loggerMock = new Mock<ILogger<ContentService>>();
            var contentService = new ContentService(configurationMock.Object, loggerMock.Object);

            // Act
            contentService.SaveContent("Test content");

            // Assert
            configurationMock.Verify(config => config["FilePath"], Times.Once);
            loggerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void SaveContent_ExceptionMissingFilePath()
        {
            // Arrange
            var configurationMock = new Mock<IConfiguration>();
            configurationMock.Setup(config => config["FilePath"]).Returns("");

            var loggerMock = new Mock<ILogger<ContentService>>();
            var contentService = new ContentService(configurationMock.Object, loggerMock.Object);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => contentService.SaveContent("Test content with exception"));
        }
    }
}
