using Azure.Storage.Blobs;
using Interfaces.Persistence.BlobStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.BlobStorage
{
    public class BlobStorageRepository : IBlobStorageRepository
    {
        private readonly BlobServiceClient _blobServiceClient;

        public BlobStorageRepository(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        public async Task SaveWeatherData(string weatherData)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient("weather-data");
            await containerClient.CreateIfNotExistsAsync();

            string blobName = $"weather-{DateTime.UtcNow:yyyy-MM-dd-HH-mm-ss}.json";

            var blobClient = containerClient.GetBlobClient(blobName);
            byte[] data = Encoding.UTF8.GetBytes(weatherData);

            using (var stream = new MemoryStream(data))
            {
                await blobClient.UploadAsync(stream);
            }
        }
    }
}
