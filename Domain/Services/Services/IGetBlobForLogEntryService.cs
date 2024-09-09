namespace Domain.Services.Services
{
    public interface IGetBlobForLogEntryService
    {
        Task<Stream> Execute(string partitionKey, string rowKey, CancellationToken ct);
    }
}