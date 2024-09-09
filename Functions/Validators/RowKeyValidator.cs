using Domain.Functions.Validators.Models;
using Domain.Functions.Validators;
using System;
using System.Globalization;

namespace Functions.Validators
{
    public class RowKeyValidator : IRowKeyValidator
    {
        public ValidationResult<string> Validate(string rowKey, string rowKeyName)
        {
            var dateTimeFormat = "HHmmss";
            var result = new ValidationResult<string>();

            if (string.IsNullOrWhiteSpace(rowKey))
            {
                result.ValidationMessages += $"'{rowKeyName}' is null or whitespace. ";
                return result;
            }

            if (!DateTime.TryParseExact(rowKey, dateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
            {
                result.ValidationMessages += $"'{rowKeyName}' format is not '{dateTimeFormat}'. ";
                return result;
            }

            result.Success = true;
            result.Value = rowKey;

            return result;
        }
    }
}
