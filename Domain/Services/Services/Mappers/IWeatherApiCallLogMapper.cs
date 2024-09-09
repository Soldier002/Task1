using Domain.Persistence.TableStorage.Models.Entities;

namespace Domain.Services.Services.Mappers
{
    public interface IWeatherApiCallLogMapper
    {
        WeatherApiCallLog Map(HttpResponseMessage httpResponseMessage, DateTime dateTime, string blobName);
    }
}