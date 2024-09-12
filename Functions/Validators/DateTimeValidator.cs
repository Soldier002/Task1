using System;
using System.Globalization;
using Domain.Common.Strings;
using Domain.Functions.Validators;
using Domain.Functions.Validators.Models;

namespace Functions.Validators
{
    public class DateTimeValidator : IDateTimeValidator
    {
        public ValidationResult<DateTime> Validate(string dateTimeStr, string dateTimeStrName)
        {
            var dateTimeFormat = DateTimeFormats.Iso8601Format;
            var result = new ValidationResult<DateTime>();

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
