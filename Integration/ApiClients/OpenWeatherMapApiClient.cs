using Common.Strings;
using Domain.Common.Configuration;
using Domain.Integration.ApiClients;

namespace Integration.ApiClients
{
    public class OpenWeatherMapApiClient : IOpenWeatherMapApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _weatherApiUrl = "https://api.openweathermap.org/data/2.5/weather?q=London&appid=";
        private readonly IConfigurationManager _configurationManager;

        public OpenWeatherMapApiClient(IHttpClientFactory httpClientFactory, IConfigurationManager configurationManager)
        {
            _httpClient = httpClientFactory.CreateClient(HttpClientNames.WeatherApi);
            _configurationManager = configurationManager;
        }

        public async Task<HttpResponseMessage> GetWeatherInLondon()
        {
            var response = await _httpClient.GetAsync($"{_weatherApiUrl}{_configurationManager.WeatherApiKey}");

            return response;
        }
    }
}
