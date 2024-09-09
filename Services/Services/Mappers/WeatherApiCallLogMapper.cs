using Domain.Persistence.TableStorage.Mappers;
using Domain.Persistence.TableStorage.Models.Entities;
using Domain.Services.Services.Mappers;

namespace Services.Services.Mappers
{
    public class WeatherApiCallLogMapper : IWeatherApiCallLogMapper
    {
        private readonly IKeysMapper _keysMapper;

        public WeatherApiCallLogMapper(IKeysMapper keysMapper)
        {
            _keysMapper = keysMapper;
        }

        public WeatherApiCallLog Map(HttpResponseMessage httpResponseMessage, DateTime dateTime, string blobName)
        {
            var keys = _keysMapper.Map(dateTime);

            var weatherApiCallLog = new WeatherApiCallLog
            {
                Success = httpResponseMessage.IsSuccessStatusCode,
                HttpStatusCode = (int)httpResponseMessage.StatusCode,
                RowKey = keys.RowKey,
                PartitionKey = keys.PartitionKey,
                PayloadBlobName = blobName,
            };

            return weatherApiCallLog;
        }
    }
}
