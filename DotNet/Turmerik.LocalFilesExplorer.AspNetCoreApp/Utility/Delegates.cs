using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.Utility
{
    public delegate int IdxRetriever<T, TNmrbl>(TNmrbl nmrbl, int count) where TNmrbl : IEnumerable<T>;
}
