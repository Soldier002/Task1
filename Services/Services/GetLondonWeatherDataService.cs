using Infrastructure.ApiClients;
using Interfaces.Infrastructure.ApiClients;
using Interfaces.Persistence.BlobStorage;
using Interfaces.Persistence.TableStorage.Models.Entities;
using Interfaces.Persistence.TableStorage.Repositories;
using Interfaces.Services.Services;
using System.Text;

namespace Services.Services
{
    public class GetLondonWeatherDataService : IGetLondonWeatherDataService
    {
        private readonly IOpenWeatherMapApiClient _openWeatherMapApiClient;
        private readonly IBlobStorageRepository _blobStorageRepository;
        private readonly ITableStorageRepository _tableStorageRepository;

        public GetLondonWeatherDataService(IOpenWeatherMapApiClient openWeatherMapApiClient, IBlobStorageRepository blobStorageRepository, ITableStorageRepository tableStorageRepository)
        {
            _openWeatherMapApiClient = openWeatherMapApiClient;
            _blobStorageRepository = blobStorageRepository;
            _tableStorageRepository = tableStorageRepository;
        }

        public async Task Execute(DateTime executionDateTime)
        {
            using var weatherApiResponse = await _openWeatherMapApiClient.GetWeatherInLondon();

            var weatherApiCallLog = new WeatherApiCallLog
            {
                Success = weatherApiResponse.IsSuccessStatusCode,
                HttpStatusCode = (int)weatherApiResponse.StatusCode,
                RowKey = executionDateTime.ToString("HHmmss"),
                PartitionKey = executionDateTime.ToString("yyyyMMdd")
            };

            var weatherDataStream = weatherApiResponse.Content.ReadAsStream();
            var blobName = await _blobStorageRepository.SaveWeatherData(weatherDataStream, executionDateTime);
            weatherApiCallLog.PayloadBlobName = blobName;
            await _tableStorageRepository.Save(weatherApiCallLog);
        }
    }
}
