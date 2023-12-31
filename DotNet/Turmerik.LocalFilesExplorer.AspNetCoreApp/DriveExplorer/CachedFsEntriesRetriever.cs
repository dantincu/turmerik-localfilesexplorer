using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.DriveExplorer
{
    public interface ICachedFsEntriesRetriever : ICachedEntriesRetriever
    {
    }

    public class CachedFsEntriesRetriever : CachedEntriesRetrieverBase, ICachedFsEntriesRetriever
    {
        private readonly char dirSeparator;
        private readonly string dirSepStr;

        public CachedFsEntriesRetriever(
            DriveItem rootFolder,
            char dirSeparator) : base(rootFolder)
        {
            this.dirSeparator = dirSeparator;
            this.dirSepStr = dirSeparator.ToString();
        }

        public override async Task<bool> FileExistsAsync(
            string idnf)
        {
            var item = GetItem(idnf, false, false);
            return item != null;
        }

        public override async Task<bool> FolderExistsAsync(string idnf)
        {
            var item = GetItem(idnf, true, false);
            return item != null;
        }

        public override async Task<byte[]?> GetFileBytesAsync(string idnf)
        {
            byte[] retArr = null;
            var item = GetItem(idnf, false, false);

            if (item != null)
            {
                retArr = new byte[0];
            }

            return retArr;
        }

        public override async Task<string?> GetFileTextAsync(string idnf)
        {
            string? retStr = null;
            var item = GetItem(idnf, false, false) as TextFile;

            if (item != null)
            {
                retStr = item.TextFileContents;
            }

            return retStr;
        }

        public override async Task<DriveItem> GetItemAsync(
            string idnf, bool retMinimalInfo) => GetItem(
                idnf, false, retMinimalInfo);

        public override async Task<DriveItem?> GetFolderAsync(
            string idnf, bool retMinimalInfo) => GetItem(idnf, true, retMinimalInfo);

        public override string GetItemIdnf<TDriveItem>(
            TDriveItem item,
            string prIdnf)
        {
            string idnf = item.Idnf;
            prIdnf ??= item.PrIdnf;

            if (idnf == null && prIdnf != null)
            {
                idnf = Path.Combine(
                    prIdnf,
                    item.Name);
            }

            return idnf;
        }

        public override async Task<bool> ItemExistsAsync(
            string idnf)
        {
            bool exists = await FolderExistsAsync(idnf);
            exists = exists || await FileExistsAsync(idnf);

            return exists;
        }

        protected override char GetDirSeparator() => dirSeparator;

        protected DriveItem GetItem(
            string idnf,
            bool? requireFolder,
            bool retMinimalInfo)
        {
            DriveItem? item;

            string? rootFolderPath = RootFolder.Idnf?.Trim(
                dirSeparator) ?? string.Empty;

            if (rootFolderPath == idnf)
            {
                if (requireFolder != false)
                {
                    item = RootFolder;
                }
                else
                {
                    item = null;
                }
            }
            else
            {
                rootFolderPath += dirSepStr;

                if (idnf.StartsWith(rootFolderPath))
                {
                    string relPath = idnf.Substring(
                        rootFolderPath.Length);

                    if (string.IsNullOrEmpty(relPath))
                    {
                        if (requireFolder != false)
                        {
                            item = RootFolder;
                        }
                        else
                        {
                            item = null;
                        }
                    }
                    else
                    {
                        item = GetItem(
                            RootFolder,
                            relPath,
                            requireFolder);
                    }
                }
                else
                {
                    item = null;
                }
            }

            RemoveAdditionalInfoIfReq(item, retMinimalInfo);
            return item;
        }

        protected DriveItem? GetItem(
            DriveItem parentFolder,
            string relPath,
            bool? requireFolder)
        {
            DriveItem? retItem;
            string[] pathParts = relPath.Split(dirSeparator);

            if (pathParts.Length == 1)
            {
                string name = pathParts.Single();

                retItem = GetItemCore(
                    parentFolder, name, requireFolder);
            }
            else
            {
                string newParentName = pathParts.First();

                var newParent = GetItemCore(
                    parentFolder, newParentName, true);

                if (newParent != null)
                {
                    string childRelPath = string.Join(
                        dirSepStr, pathParts.Skip(1).ToArray());

                    retItem = GetItemCore(
                        newParent,
                        childRelPath,
                        requireFolder);
                }
                else
                {
                    retItem = null;
                }
            }

            return retItem;
        }

        protected DriveItem? GetItemCore(
            DriveItem parentFolder,
            string name,
            bool? requireFolder)
        {
            DriveItem? item;

            if (requireFolder.HasValue)
            {
                if (requireFolder.Value)
                {
                    item = GetItemCore(
                        parentFolder.SubFolders,
                        name);
                }
                else
                {
                    item = GetItemCore(
                        parentFolder.FolderFiles,
                        name);
                }
            }
            else
            {
                item = GetItemCore(
                    parentFolder.SubFolders,
                    name);

                item ??= GetItemCore(
                    parentFolder.FolderFiles,
                    name);
            }

            return item;
        }

        protected DriveItem? GetItemCore(
            List<DriveItem>? childItems,
            string name) => childItems?.FirstOrDefault(
                item => item.Name == name);
    }
}
