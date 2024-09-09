using Domain.Functions.Validators;
using Domain.Functions.Validators.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Functions.Validators
{
    public class PartitionKeyValidator : IPartitionKeyValidator
    {
        public ValidationResult<string> Validate(string partitionKey, string partitionKeyName)
        {
            var dateTimeFormat = "yyyyMMdd";
            var result = new ValidationResult<string>();

            if (string.IsNullOrWhiteSpace(partitionKey))
            {
                result.ValidationMessages += $"'{partitionKeyName}' is null or whitespace. ";
                return result;
            }

            if (!DateTime.TryParseExact(partitionKey, dateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
            {
                result.ValidationMessages += $"'{partitionKeyName}' format is not '{dateTimeFormat}'. ";
                return result;
            }

            result.Success = true;
            result.Value = partitionKey;

            return result;
        }
    }
}
