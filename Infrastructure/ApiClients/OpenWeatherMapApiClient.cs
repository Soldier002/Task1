using Interfaces.Infrastructure.ApiClients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ApiClients
{
    public class OpenWeatherMapApiClient : IOpenWeatherMapApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _weatherApi = "https://api.openweathermap.org/data/2.5/weather?q=London&appid=faa625de9ce05a0abdf9cf5850ca5637";

        public OpenWeatherMapApiClient(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<HttpResponseMessage> GetWeatherInLondon()
        {
            var response = await _httpClient.GetAsync(_weatherApi);

            return response;
        }
    }
}
