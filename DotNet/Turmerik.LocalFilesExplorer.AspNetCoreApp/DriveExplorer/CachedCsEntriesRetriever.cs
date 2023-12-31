using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.DriveExplorer
{
    public interface ICachedCsEntriesRetriever : ICachedEntriesRetriever
    {
    }

    public class CachedCsEntriesRetriever : CachedEntriesRetrieverBase, ICachedCsEntriesRetriever
    {
        public CachedCsEntriesRetriever(DriveItem rootFolder) : base(rootFolder)
        {
        }

        public override async Task<bool> FileExistsAsync(string idnf)
        {
            var item = await GetItemAsync(idnf, false);
            bool retVal = item != null && item.IsFolder != true;

            return retVal;
        }

        public override async Task<bool> FolderExistsAsync(string idnf)
        {
            var item = await GetItemAsync(idnf, false);
            return item?.IsFolder == true;
        }

        public override async Task<byte[]> GetFileBytesAsync(string idnf)
        {
            var item = await GetItemAsync(idnf, false);
            byte[] retArr = null;

            if (item != null && item.IsFolder != true)
            {
                retArr = new byte[0];
            }

            return retArr;
        }

        public override async Task<string> GetFileTextAsync(string idnf)
        {
            var item = await GetItemAsync(idnf, false);
            string retStr = null;

            if (item != null && item.IsFolder != true)
            {
                retStr = string.Empty;
            }

            return retStr;
        }

        public override async Task<DriveItem> GetFolderAsync(
            string idnf, bool retMinimalInfo)
        {
            var item = await GetItemAsync(idnf, retMinimalInfo);
            item = NullifyIfReq(item, true);

            return item;
        }

        public override async Task<DriveItem> GetItemAsync(
            string idnf, bool retMinimalInfo)
        {
            var itemsNmrbl = EnumerateItems(RootFolder, null);

            var retItem = itemsNmrbl.FirstOrDefault(
                item => item.Idnf == idnf);

            return retItem;
        }

        public override string GetItemIdnf<TDriveItem>(
            TDriveItem item,
            string prIdnf) => item.Idnf;

        public override async Task<bool> ItemExistsAsync(string idnf)
        {
            var item = await GetItemAsync(idnf, true);
            return item != null;
        }

        protected override char GetDirSeparator() => '/';
    }
}
