using Domain.Functions.Validators;
using Domain.Functions.Validators.Models; 
using System;
using System.Globalization;

namespace Functions.Validators
{
    public class DateTimeRangeValidator : IDateTimeRangeValidator
    {
        public DateTimeRangeValidationResult Validate(string from, string to, string fromName, string toName)
        {
            var fromValidationResult = ValidateDateTime(from, fromName);
            var toValidationResult = ValidateDateTime(to, toName);
            var result = new DateTimeRangeValidationResult();

            if (!(fromValidationResult.Success && toValidationResult.Success))
            {
                result.ValidationMessages = fromValidationResult.ValidationMessages + toValidationResult.ValidationMessages;
                return result;
            }

            if (fromValidationResult.Value > toValidationResult.Value)
            {
                result.ValidationMessages = $@"'{fromName}' should not be later than '{toName}'. ";
                return result;
            }

            result.Success = true;
            result.From = fromValidationResult.Value;
            result.To = toValidationResult.Value;

            return result;
        }

        private DateTimeValidationResult ValidateDateTime(string dateTimeStr, string dateTimeStrName)
        {
            var dateTimeFormat = "yyyy-MM-ddTHH:mm:ss";
            var validationMessages = string.Empty;
            var result = new DateTimeValidationResult();

            if (string.IsNullOrWhiteSpace(dateTimeStr))
            {
                result.ValidationMessages += $"'{dateTimeStrName}' is null or whitespace. ";
                return result;
            }

            if (!DateTime.TryParseExact(dateTimeStr, dateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var dateTime))
            {
                result.ValidationMessages += $"'{dateTimeStrName}' format is not '{dateTimeFormat}'. ";
                return result;
            }

            result.Success = true;
            result.Value = dateTime;

            return result;
        }
    }
}
