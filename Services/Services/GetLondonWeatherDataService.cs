using Ardalis.GuardClauses;
using Domain.Integration.ApiClients;
using Domain.Persistence.BlobStorage.Builders;
using Domain.Persistence.BlobStorage.Repositories;
using Domain.Persistence.TableStorage.Models.Entities;
using Domain.Persistence.TableStorage.Repositories;
using Domain.Services.Services;
using Domain.Services.Services.Mappers;

namespace Services.Services
{
    public class GetLondonWeatherDataService : IGetLondonWeatherDataService
    {
        private readonly IOpenWeatherMapApiClient _openWeatherMapApiClient;
        private readonly IBlobStorageRepository _blobStorageRepository;
        private readonly ITableStorageRepository _tableStorageRepository;
        private readonly IWeatherApiCallLogMapper _weatherApiCallLogMapper;
        private readonly IBlobNameBuilder _blobNameBuilder;

        public GetLondonWeatherDataService(IOpenWeatherMapApiClient openWeatherMapApiClient, IBlobStorageRepository blobStorageRepository, ITableStorageRepository tableStorageRepository, IWeatherApiCallLogMapper weatherApiCallLogMapper, IBlobNameBuilder blobNameBuilder)
        {
            _openWeatherMapApiClient = openWeatherMapApiClient;
            _blobStorageRepository = blobStorageRepository;
            _tableStorageRepository = tableStorageRepository;
            _weatherApiCallLogMapper = weatherApiCallLogMapper;
            _blobNameBuilder = blobNameBuilder;
        }

        public async Task Execute(DateTime executionDateTime, CancellationToken ct)
        {
            using var weatherApiResponse = await _openWeatherMapApiClient.GetWeatherInLondon(ct);
            var weatherApiCallLog = _weatherApiCallLogMapper.Map(weatherApiResponse, executionDateTime, _blobNameBuilder.Build(executionDateTime));

            if (weatherApiResponse.IsSuccessStatusCode)
            {
                Guard.Against.Null(weatherApiResponse.Content);
                using var weatherDataStream = await weatherApiResponse.Content.ReadAsStreamAsync(ct);
                await _blobStorageRepository.SaveWeatherData(weatherDataStream, executionDateTime, ct);
            }
            else
            {
                weatherApiCallLog.PayloadBlobName = null!;
            }

            await _tableStorageRepository.Save(weatherApiCallLog, ct);
        }
    }
}
