using Interfaces.Persistence.TableStorage.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Persistence.TableStorage.Mappers
{
    public interface IKeysMapper
    {
        Keys Map(DateTime dateTime);
    }
}
