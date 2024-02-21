namespace PvStorageService.Application.Services
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;

    public class ContentService : IContentService
    {
        private readonly IConfiguration configuration;
        private readonly ILogger<ContentService> logger;

        public ContentService(IConfiguration configuration, ILogger<ContentService> logger)
        {
            this.configuration = configuration;
            this.logger = logger;
        }

        public void SaveContent(string content)
        {
            try
            {
                string filePath = this.configuration["FilePath"];

                if (!File.Exists(filePath))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                };

                File.AppendAllText(filePath, content + Environment.NewLine);

            }
            catch (Exception ex)
            {
                this.logger.LogError($"Failed to save content to file > Content: {content}", ex);
                throw;
            }

        }
    }
}
