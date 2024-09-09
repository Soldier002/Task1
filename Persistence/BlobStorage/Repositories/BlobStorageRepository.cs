using Domain.Persistence.BlobStorage.Clients;
using Domain.Persistence.BlobStorage.Repositories;

namespace Persistence.BlobStorage.Repositories
{
    public class BlobStorageRepository : IBlobStorageRepository
    {
        private readonly IBlobContainerClientFactory _blobContainerClientFactory;

        public BlobStorageRepository(IBlobContainerClientFactory blobContainerClientFactory)
        {
            _blobContainerClientFactory = blobContainerClientFactory;
        }

        public async Task<string> SaveWeatherData(Stream weatherData, DateTime now)
        {
            var containerClient = await _blobContainerClientFactory.Create();

            string blobName = $"weather-{now:yyyy-MM-dd-HH-mm-ss}.json";
            var blobClient = containerClient.GetBlobClient(blobName);
            await blobClient.UploadAsync(weatherData);

            return blobName;
        }
    }
}
