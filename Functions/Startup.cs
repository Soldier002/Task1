using Azure.Data.Tables;
using Azure.Storage.Blobs;
using Functions;
using Infrastructure.ApiClients;
using Interfaces.Infrastructure.ApiClients;
using Interfaces.Persistence.BlobStorage;
using Interfaces.Persistence.BlobStorage.Clients;
using Interfaces.Persistence.TableStorage.Clients;
using Interfaces.Persistence.TableStorage.Mappers;
using Interfaces.Persistence.TableStorage.Repositories;
using Interfaces.Services.Services;
using Interfaces.Utils.Configuration;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.DependencyInjection;
using Persistence.BlobStorage;
using Persistence.BlobStorage.Clients;
using Persistence.TableStorage.Clients;
using Persistence.TableStorage.Mappers;
using Persistence.TableStorage.Repositories;
using Services.Services;
using System;
using Utils.Configuration;

[assembly: FunctionsStartup(typeof(Startup))]

namespace Functions;

public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        var configurationManager = new ConfigurationManager();

        builder.Services.AddHttpClient();
        builder.Services.AddSingleton(_ => new TableClient(configurationManager.AzureWebJobsStorage, configurationManager.TableClientName));
        builder.Services.AddSingleton(_ => new BlobContainerClient(configurationManager.AzureWebJobsStorage, configurationManager.BlobContainerName));
        builder.Services.AddSingleton<ITableClientFactory, TableClientFactory>();
        builder.Services.AddSingleton<IBlobContainerClientFactory, BlobContainerClientFactory>();

        builder.Services.AddTransient<IConfigurationManager, ConfigurationManager>();
        builder.Services.AddTransient<IGetLondonWeatherDataService, GetLondonWeatherDataService>();
        builder.Services.AddTransient<IBlobStorageRepository, BlobStorageRepository>();
        builder.Services.AddTransient<ITableStorageRepository, TableStorageRepository>();
        builder.Services.AddTransient<IOpenWeatherMapApiClient, OpenWeatherMapApiClient>();
        builder.Services.AddTransient<IGetLogsForPeriodService, GetLogsForPeriodService>();
        builder.Services.AddTransient<IKeysMapper, KeysMapper>();
    }
}