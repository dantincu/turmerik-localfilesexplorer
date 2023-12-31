using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Turmerik.LocalFilesExplorer.AspNetCoreApp.Helpers;
using Turmerik.LocalFilesExplorer.AspNetCoreApp.Utility;

namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.DriveExplorer
{
    public static class DriveExplorerH
    {
        public const int DEFAULT_ENTRY_NAME_MAX_LENGTH = 100;

        public const string DIR_PAIRS_CFG_FILE_NAME = "trmrk-dirpairs-config.json";

        public static void CopyChildren<TDriveItem>(
            DriveItem<TDriveItem> destn,
            List<TDriveItem>? srcFolders,
            List<TDriveItem>? srcFiles,
            int depth = 0)
            where TDriveItem : DriveItem<TDriveItem>
        {
            if (depth > 0)
            {
                int childrenDepth = depth - 1;

                destn.SubFolders = srcFolders?.Select(
                    item => item.CreateFromSrc<TDriveItem>(null, childrenDepth)).ToList();

                destn.FolderFiles = srcFiles?.Select(
                    item => item.CreateFromSrc<TDriveItem>(null, 0)).ToList();
            }
            else if (depth < 0)
            {
                destn.SubFolders = srcFolders;
                destn.FolderFiles = srcFiles;
            }
        }

        public static DriveItemX ToItemX(
            this DriveItem src,
            int depth = 0) => new DriveItemX(
                src, depth);
    }
}
