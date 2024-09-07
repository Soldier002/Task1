using Infrastructure.ApiClients;
using Interfaces.Infrastructure.ApiClients;
using Interfaces.Persistence.BlobStorage;
using Interfaces.Services.Services;
using Persistence.BlobStorage;
using System.Text;

namespace Services.Services
{
    public class GetLondonWeatherDataService : IGetLondonWeatherDataService
    {
        private readonly IOpenWeatherMapApiClient _openWeatherMapApiClient;
        private readonly IBlobStorageRepository _blobStorageRepository;

        public GetLondonWeatherDataService(IOpenWeatherMapApiClient openWeatherMapApiClient, IBlobStorageRepository blobStorageRepository)
        {
            _openWeatherMapApiClient = openWeatherMapApiClient;
            _blobStorageRepository = blobStorageRepository;
        }

        public async Task Execute()
        {
            var weatherInLondonJson = await _openWeatherMapApiClient.GetWeatherInLondon();
            await _blobStorageRepository.SaveWeatherData(weatherInLondonJson);
        }
    }
}
