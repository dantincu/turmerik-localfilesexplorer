using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.DriveExplorer
{
    public interface IDriveItemXData
    {
        bool? IsCreated { get; set; }
        bool? IsRemoved { get; set; }
        bool? Remove { get; set; }
        string RenameTo { get; set; }
        string MoveToPrIdnf { get; set; }
        string IsRenamedFrom { get; set; }
        string IsMovedFromPrIdnf { get; set; }
        string TextFileContents { get; set; }
    }

    public class DriveItemXData : IDriveItemXData
    {
        public bool? IsCreated { get; set; }
        public bool? IsRemoved { get; set; }
        public bool? Remove { get; set; }
        public string RenameTo { get; set; }
        public string MoveToPrIdnf { get; set; }
        public string IsRenamedFrom { get; set; }
        public string IsMovedFromPrIdnf { get; set; }
        public string TextFileContents { get; set; }
    }

    public class DriveItemX : DriveItem<DriveItemX, DriveItemXData>
    {
        public DriveItemX()
        {
        }

        public DriveItemX(
            DriveItem src, int depth = 0) : base(src)
        {
            DriveExplorerH.CopyChildren(
                this,
                src.SubFolders?.Select(
                    item => new DriveItemX(
                        item, depth - 1)).ToList(),
                src.FolderFiles?.Select(
                    item => new DriveItemX(
                        item, depth - 1)).ToList(),
                depth);
        }
    }
}
