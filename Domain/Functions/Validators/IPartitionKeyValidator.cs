using Domain.Functions.Validators.Models;

namespace Domain.Functions.Validators
{
    public interface IPartitionKeyValidator
    {
        ValidationResult<string> Validate(string partitionKey, string partitionKeyName);
    }
}