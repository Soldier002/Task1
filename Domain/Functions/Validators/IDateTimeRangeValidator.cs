using Domain.Functions.Validators.Models;

namespace Domain.Functions.Validators
{
    public interface IDateTimeRangeValidator
    {
        DateTimeRangeValidationResult Validate(string from, string to, string fromName, string toName);
    }
}