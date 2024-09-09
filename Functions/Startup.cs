using Azure.Data.Tables;
using Azure.Storage.Blobs;
using Common.Configuration;
using Common.Parsers;
using Domain.Common.Configuration;
using Domain.Common.Parsers;
using Domain.Integration.ApiClients;
using Domain.Persistence.BlobStorage.Clients;
using Domain.Persistence.BlobStorage.Repositories;
using Domain.Persistence.TableStorage.Clients;
using Domain.Persistence.TableStorage.Mappers;
using Domain.Persistence.TableStorage.Repositories;
using Domain.Services.Services;
using Functions;
using Integration.ApiClients;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Persistence.BlobStorage.Clients;
using Persistence.BlobStorage.Repositories;
using Persistence.TableStorage.Clients;
using Persistence.TableStorage.Mappers;
using Persistence.TableStorage.Repositories;
using Services.Services;

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
        builder.Services.AddTransient<IDateTimeParser, DateTimeParser>();
    }
}