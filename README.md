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
## Possible future developments
- Docker container support
- Integration tests for the whole app and persistence layer specifically
- Refactoring functions to isolated worker model
- Integrating with the actual azure
- Logging exceptions persistently
