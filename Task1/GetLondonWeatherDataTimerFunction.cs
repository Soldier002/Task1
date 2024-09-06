using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace Task1
{
    public class GetLondonWeatherDataTimerFunction
    {
        private readonly HttpClient _client;
        private readonly string _weatherApi = "https://api.openweathermap.org/data/2.5/weather?q=London&appid=faa625de9ce05a0abdf9cf5850ca5637";
        private readonly BlobServiceClient _blobServiceClient;

        public GetLondonWeatherDataTimerFunction(IHttpClientFactory httpClientFactory, BlobServiceClient blobServiceClient)
        {
            _client = httpClientFactory.CreateClient();
            _blobServiceClient = blobServiceClient;
        }

        [FunctionName("GetLondonWeatherDataTimerFunction")]
        public async Task Run([TimerTrigger("*/5 * * * * *")]TimerInfo myTimer, ILogger log)
        {
            var response = await _client.GetAsync(_weatherApi);
            var contentString = await response.Content.ReadAsStringAsync();

            var containerClient = _blobServiceClient.GetBlobContainerClient("weather-data");
            await containerClient.CreateIfNotExistsAsync();

            string blobName = $"weather-{DateTime.UtcNow:yyyy-MM-dd-HH-mm-ss}.json";

            var blobClient = containerClient.GetBlobClient(blobName);
            byte[] data = Encoding.UTF8.GetBytes(contentString);

            using (var stream = new MemoryStream(data))
            {
                await blobClient.UploadAsync(stream);
            }
        }
    }
}
