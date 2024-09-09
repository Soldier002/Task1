using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Integration.ApiClients
{
    public interface IOpenWeatherMapApiClient
    {
        Task<HttpResponseMessage> GetWeatherInLondon(CancellationToken ct);
    }
}
