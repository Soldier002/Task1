using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Functions.Validators.Models
{
    public class DateTimeRangeValidationResult
    {
        public bool Success { get; set; }

        public string ValidationMessages { get; set; } = string.Empty;

        public DateTime From { get; set; }

        public DateTime To { get; set; }
    }
}
