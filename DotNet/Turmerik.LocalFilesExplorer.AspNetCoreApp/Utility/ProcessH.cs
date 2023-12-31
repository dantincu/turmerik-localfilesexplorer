using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.Utility
{
    public static class ProcessH
    {
        public static void OpenWithDefaultProgramIfNotNull(string path)
        {
            if (path != null)
            {
                using Process fileopener = new Process();

                fileopener.StartInfo.FileName = "explorer";
                fileopener.StartInfo.Arguments = "\"" + path + "\"";
                fileopener.Start();
            }
        }
    }
}
