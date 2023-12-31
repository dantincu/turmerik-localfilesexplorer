using System;
using System.Collections.Generic;
using System.Text;

namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.DriveExplorer
{
    public interface ICachedEntriesRetriever : IDriveItemsRetriever
    {
        DriveItem RootFolder { get; }

        IEnumerable<DriveItem> EnumerateItems(
            DriveItem parentItem,
            bool? foldersOnly);
    }

    public abstract class CachedEntriesRetrieverBase : DriveItemsRetrieverCoreBase
    {
        public CachedEntriesRetrieverBase(DriveItem rootFolder)
        {
            RootFolder = rootFolder ?? throw new ArgumentNullException(nameof(rootFolder));
        }

        public DriveItem RootFolder { get; }

        public IEnumerable<DriveItem> EnumerateItems(
            DriveItem parentItem,
            bool? foldersOnly)
        {
            var retItem = NullifyIfReq(
                parentItem, foldersOnly);

            if (retItem != null)
            {
                yield return retItem;
            }

            foreach (var item in EnumerateItemsIfReq(
                parentItem,
                foldersOnly))
            {
                yield return item;
            }
        }

        protected IEnumerable<DriveItem> EnumerateItemsIfReq(
            DriveItem parentItem,
            bool? foldersOnly)
        {
            foreach (var item in EnumerateItemsIfReqCore(
                parentItem.SubFolders,
                foldersOnly))
            {
                yield return item;
            }

            foreach (var item in EnumerateFilesIfReq(
                parentItem.FolderFiles,
                foldersOnly))
            {
                yield return item;
            }
        }

        protected IEnumerable<DriveItem> EnumerateItemsIfReqCore(
            List<DriveItem>? foldersList,
            bool? foldersOnly)
        {
            if (foldersList != null)
            {
                foreach (var subFolder in foldersList)
                {
                    foreach (var childItem in EnumerateItems(
                        subFolder, foldersOnly))
                    {
                        yield return childItem;
                    }
                }
            }
        }

        protected IEnumerable<DriveItem> EnumerateFilesIfReq(
            List<DriveItem>? children,
            bool? foldersOnly)
        {
            if (foldersOnly != null)
            {
                if (children != null)
                {
                    foreach (var child in children)
                    {
                        yield return child;
                    }
                }
            }
        }

        protected DriveItem NullifyIfReq(
            DriveItem item,
            bool? requireFolder)
        {
            if (requireFolder.HasValue)
            {
                if (requireFolder.Value)
                {
                    if (item.IsFolder == true)
                    {
                        item = null;
                    }
                }
                else
                {
                    if (item.IsFolder != true)
                    {
                        item = null;
                    }
                }
            }
            else
            {
                item = null;
            }

            return item;
        }
    }
}
