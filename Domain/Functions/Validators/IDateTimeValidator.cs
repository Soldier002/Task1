using Domain.Functions.Validators.Models;

namespace Domain.Functions.Validators
{
    public interface IDateTimeValidator
    {
        ValidationResult<DateTime> Validate(string dateTimeStr, string dateTimeStrName);
    }
}