﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common.Configuration
{
    public interface IConfigurationManager
    {
        string AzureWebJobsStorage { get; }

        string TableClientName { get; }

        string BlobContainerName { get; }

        string WeatherApiKey { get; }
    }
}
