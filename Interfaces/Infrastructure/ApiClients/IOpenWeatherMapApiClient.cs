﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Infrastructure.ApiClients
{
    public interface IOpenWeatherMapApiClient
    {
        Task<string> GetWeatherInLondon();
    }
}
