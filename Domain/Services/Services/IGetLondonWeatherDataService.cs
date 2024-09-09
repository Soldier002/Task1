using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Services
{
    public interface IGetLondonWeatherDataService
    {
        Task Execute(DateTime executionDateTime, CancellationToken ct);
    }
}
