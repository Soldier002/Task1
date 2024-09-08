using Azure.Storage.Blobs;
using Functions;
using Infrastructure.ApiClients;
using Interfaces.Infrastructure.ApiClients;
using Interfaces.Persistence.BlobStorage;
using Interfaces.Persistence.TableStorage.Repositories;
using Interfaces.Services.Services;
using Interfaces.Utils.Configuration;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.DependencyInjection;
using Persistence.BlobStorage;
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
        builder.Services.AddHttpClient();
        builder.Services.AddSingleton(_ => new BlobServiceClient(Environment.GetEnvironmentVariable("AzureWebJobsStorage")));
        builder.Services.AddTransient<IConfigurationManager, ConfigurationManager>();
        builder.Services.AddTransient<IGetLondonWeatherDataService, GetLondonWeatherDataService>();
        builder.Services.AddTransient<IBlobStorageRepository, BlobStorageRepository>();
        builder.Services.AddTransient<ITableStorageRepository, TableStorageRepository>();
        builder.Services.AddTransient<IOpenWeatherMapApiClient, OpenWeatherMapApiClient>();
    }
}