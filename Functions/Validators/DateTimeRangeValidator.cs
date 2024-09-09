using Domain.Functions.Validators;
using Domain.Functions.Validators.Models;
using System;
using System.Globalization;

namespace Functions.Validators
{
    public class DateTimeRangeValidator : IDateTimeRangeValidator
    {
        private readonly IDateTimeValidator _dateTimeValidator;

        public DateTimeRangeValidator(IDateTimeValidator dateTimeValidator)
        {
            _dateTimeValidator = dateTimeValidator;
        }

        public DateTimeRangeValidationResult Validate(string from, string to, string fromName, string toName)
        {
            var fromValidationResult = _dateTimeValidator.Validate(from, fromName);
            var toValidationResult = _dateTimeValidator.Validate(to, toName);
            var result = new DateTimeRangeValidationResult();

            if (!(fromValidationResult.Success && toValidationResult.Success))
            {
                result.ValidationMessages = fromValidationResult.ValidationMessages + toValidationResult.ValidationMessages;
                return result;
            }

            if (fromValidationResult.Value > toValidationResult.Value)
            {
                result.ValidationMessages = $"'{fromName}' should not be later than '{toName}'. ";
                return result;
            }

            result.Success = true;
            result.From = fromValidationResult.Value;
            result.To = toValidationResult.Value;

            return result;
        }
    }
}
