using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(Exception ex)
            : base("item was not found", ex)
        {
        }
    }
}
