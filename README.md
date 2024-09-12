## Launching instructions
- Open with Visual Studio 2022
- Make sure Azurite is running
- Add local.settings.json with {...} to Functions project
```
{
  "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "UseDevelopmentStorage=true",
    "TableClientName": "weatherData",
    "BlobContainerName": "weather-data",
    "WeatherApiKey": "", // https://home.openweathermap.org/api_keys
    "FUNCTIONS_WORKER_RUNTIME": "dotnet"
  }
}
```
## Calling HttpTrigger functions
- Call the function for getting api call logs as shown below. from and to are dates in format yyyy-MM-ddTHH:mm:ss
```
http://localhost:7298/api/GetLogsForPeriodFunction?from=2024-09-10T08:48:50&to=2024-09-12T05:00:00
```
- Call the function for downloading blob for specific log entry as shown below. partitionKey and rowKey are partition and row keys of call log entity from GetLogsForPeriodFunction.
```
{
  "RowKey": "000550",
  "PartitionKey": "20240912",
...
},
```
```
http://localhost:7298/api/GetBlobForLogEntryFunction?partitionKey=20240912&rowKey=000558
```

## Possible future developments
- Docker container support
- Integration tests for the whole app and persistence layer specifically
- Refactoring functions to isolated worker model
- Integrating with the actual azure
- Logging exceptions persistently
