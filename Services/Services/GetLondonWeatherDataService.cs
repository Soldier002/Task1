﻿using Ardalis.GuardClauses;
using Domain.Integration.ApiClients;
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

        public GetLondonWeatherDataService(IOpenWeatherMapApiClient openWeatherMapApiClient, IBlobStorageRepository blobStorageRepository, ITableStorageRepository tableStorageRepository, IWeatherApiCallLogMapper weatherApiCallLogMapper)
        {
            _openWeatherMapApiClient = openWeatherMapApiClient;
            _blobStorageRepository = blobStorageRepository;
            _tableStorageRepository = tableStorageRepository;
            _weatherApiCallLogMapper = weatherApiCallLogMapper;
        }

        public async Task Execute(DateTime executionDateTime)
        {
            using var weatherApiResponse = await _openWeatherMapApiClient.GetWeatherInLondon();
            using var weatherDataStream = await weatherApiResponse.Content.ReadAsStreamAsync();
            var blobName = await _blobStorageRepository.SaveWeatherData(weatherDataStream, executionDateTime);
            var weatherApiCallLog = _weatherApiCallLogMapper.Map(weatherApiResponse, executionDateTime, blobName);
            await _tableStorageRepository.Save(weatherApiCallLog);
        }
    }
}
