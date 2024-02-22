namespace Services.Storage
{
    using Grpc.Core;
    using PvStorageService;
    using PvStorageService.Application.Services;

    public class StorageService : Storage.StorageBase
    {
        private readonly IContentFileService contentService;
        private readonly ILogger<StorageService> logger;

        public StorageService(ILogger<StorageService> logger, IContentFileService contentService)
        {
            this.logger = logger;
            this.contentService = contentService;
        }

        public override Task<DataResponse> StoreData(DataRequest request, ServerCallContext context)
        {
            try
            {
                this.logger.LogInformation(request.UserAgent, request.IpAddress, request.Referer);

                if (string.IsNullOrEmpty(request.IpAddress))
                {
                    throw new RpcException(new Status(StatusCode.InvalidArgument, "The IP Address field is mandatory."));
                }

                string dataLine = FormatData(request);

                this.contentService.SaveContent(dataLine);

                return Task.FromResult(new DataResponse
                {
                    Message = "Success! Data Saved"
                });

            }
            catch (Exception ex)
            {
                this.logger.LogError($"Error storing data: {ex.Message}", ex);
                throw;
            }
            
        }

        private static string FormatData(DataRequest request)
        {
            string formattedDate = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffffffZ");
            string formattedReferer = string.IsNullOrEmpty(request.Referer) ? "null" : request.Referer;
            string formattedUserAgent = string.IsNullOrEmpty(request.UserAgent) ? "null" : request.UserAgent;

            string dataLine = $"{formattedDate}|{formattedReferer}|{formattedUserAgent}|{request.IpAddress}";

            return dataLine;
        }
    }
}
