using Azure.Storage.Blobs;
using Interfaces.Persistence.BlobStorage;
using Interfaces.Persistence.BlobStorage.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.BlobStorage
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
