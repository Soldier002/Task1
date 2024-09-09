using Domain.Persistence.BlobStorage.Builders;
using Domain.Persistence.BlobStorage.Clients;
using Domain.Persistence.BlobStorage.Repositories;

namespace Persistence.BlobStorage.Repositories
{
    public class BlobStorageRepository : IBlobStorageRepository
    {
        private readonly IBlobContainerClientFactory _blobContainerClientFactory;
        private readonly IBlobNameBuilder _blobNameBuilder;

        public BlobStorageRepository(IBlobContainerClientFactory blobContainerClientFactory, IBlobNameBuilder blobNameBuilder)
        {
            _blobContainerClientFactory = blobContainerClientFactory;
            _blobNameBuilder = blobNameBuilder;
        }

        public async Task<string> SaveWeatherData(Stream weatherData, DateTime now)
        {
            var containerClient = await _blobContainerClientFactory.Create();

            string blobName = _blobNameBuilder.Build(now);
            var blobClient = containerClient.GetBlobClient(blobName);
            await blobClient.UploadAsync(weatherData);

            return blobName;
        }

        public async Task<Stream> GetWeatherData(string blobName)
        {
            var containerClient = await _blobContainerClientFactory.Create();
            var blobClient = containerClient.GetBlobClient(blobName);
            var response = await blobClient.DownloadStreamingAsync();
            var stream = response.Value.Content;

            return stream;
        }
    }
}
