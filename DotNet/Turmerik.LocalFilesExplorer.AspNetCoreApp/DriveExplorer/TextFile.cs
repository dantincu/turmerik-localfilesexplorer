using System;
using System.Collections.Generic;
using System.Text;

namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.DriveExplorer
{
    public class TextFile : DriveItem, IDriveItemXData
    {
        public bool? IsCreated { get; set; }
        public bool? IsRemoved { get; set; }
        public bool? Remove { get; set; }
        public string RenameTo { get; set; }
        public string MoveToPrIdnf { get; set; }
        public string IsRenamedFrom { get; set; }
        public string IsMovedFromPrIdnf { get; set; }
        public string? TextFileContents { get; set; }
    }
}
