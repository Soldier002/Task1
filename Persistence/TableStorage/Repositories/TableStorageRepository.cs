using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Data.Tables;
using Interfaces.Persistence.TableStorage.Models.Entities;
using Interfaces.Persistence.TableStorage.Repositories;
using Interfaces.Utils.Configuration;

namespace Persistence.TableStorage.Repositories
{
    public class TableStorageRepository : ITableStorageRepository
    {
        private readonly IConfigurationManager _configurationManager;

        public TableStorageRepository(IConfigurationManager configurationManager)
        {
            _configurationManager = configurationManager;
        }

        public async Task Save(WeatherApiCallLog weatherApiCallLog)
        {
            var connectionString = _configurationManager.AzureWebJobsStorage;
            TableServiceClient tableServiceClient = new TableServiceClient(connectionString);
            TableClient tableClient = tableServiceClient.GetTableClient("weatherData");

            await tableClient.CreateIfNotExistsAsync();
            await tableClient.AddEntityAsync(weatherApiCallLog);
        }
    }
}
