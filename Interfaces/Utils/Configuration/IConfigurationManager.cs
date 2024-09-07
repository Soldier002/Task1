using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Utils.Configuration
{
    public interface IConfigurationManager
    {
        string AzureWebJobsStorage { get; }
    }
}
