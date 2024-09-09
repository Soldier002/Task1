using Domain.Functions.Validators.Models;

namespace Domain.Functions.Validators
{
    public interface IRowKeyValidator
    {
        ValidationResult<string> Validate(string rowKey, string rowKeyName);
    }
}