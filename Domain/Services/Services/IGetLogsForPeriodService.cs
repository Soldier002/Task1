using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Services
{
    public interface IGetLogsForPeriodService
    {
        Task<string> Execute(DateTime from, DateTime to, CancellationToken ct);
    }
}
