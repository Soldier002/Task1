using Azure;
using Azure.Data.Tables;

namespace Domain.Persistence.TableStorage.Models.Entities
{
    public class WeatherApiCallLog : ITableEntity
    {
        public string RowKey { get; set; } = default!;

        public string PartitionKey { get; set; } = default!;

        public bool Success { get; set; }

        public int HttpStatusCode { get; set; }

        public string ErrorMessage { get; set; }

        public string PayloadBlobName { get; set; }

        public DateTimeOffset? Timestamp { get; set; } = default!;

        public ETag ETag { get; set; } = default!;
    }
}
