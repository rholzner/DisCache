using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlDisCache.Domain.Helpers;
public interface IDatabaseHelper
{
    ValueTask MigrateDatabase();
}
