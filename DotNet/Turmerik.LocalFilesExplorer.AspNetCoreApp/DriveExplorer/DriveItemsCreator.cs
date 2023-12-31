using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.DriveExplorer
{
    public interface IDriveItemsCreator
    {
        Task<List<DriveItemX>> CreateItemsAsync(
            DriveItemsCreatorOpts opts);
    }

    public class DriveItemsCreator : IDriveItemsCreator
    {
        private readonly IDriveExplorerService dvExplrSvc;

        public DriveItemsCreator(
            IDriveExplorerService dvExplrSvc)
        {
            this.dvExplrSvc = dvExplrSvc ?? throw new ArgumentNullException(
                nameof(dvExplrSvc));
        }

        public async Task<List<DriveItemX>> CreateItemsAsync(
            DriveItemsCreatorOpts opts)
        {
            foreach (var item in opts.ItemsList)
            {
                item.Idnf ??= dvExplrSvc.GetItemIdnf(
                    item, opts.PrIdnf);

                var data = item.Data;

                if (data.Remove == true)
                {
                    await RemoveItemCoreAsync(item);
                }
                else if (data.IsCreated != true)
                {
                    await CreateItemCoreAsync(opts, item);
                }
                else
                {
                    bool move = !string.IsNullOrWhiteSpace(data.MoveToPrIdnf);
                    bool rename = !string.IsNullOrWhiteSpace(data.RenameTo);

                    if (move || rename)
                    {
                        await MoveItemCoreAsync(item, move, rename);
                    }
                }
            }

            return opts.ItemsList;
        }

        private async Task RemoveItemCoreAsync(
            DriveItemX item)
        {
            var data = item.Data;

            if (data.IsRemoved != true)
            {
                if (item.IsFolder == true)
                {
                    await dvExplrSvc.DeleteFolderAsync(item.Idnf, false);
                }
                else
                {
                    await dvExplrSvc.DeleteFileAsync(item.Idnf);
                }

                data.IsRemoved = true;
            }
        }

        private async Task CreateItemCoreAsync(
            DriveItemsCreatorOpts opts,
            DriveItemX item)
        {
            var data = item.Data;
            DriveItem newItem;

            if (item.IsFolder == true)
            {
                newItem = await dvExplrSvc.CreateFolderAsync(
                    opts.PrIdnf, item.Name, false);

                OnItemCreated(item, newItem);
                var childrenList = GetChildrenList(item);

                await CreateItemsAsync(new DriveItemsCreatorOpts
                {
                    ItemsList = childrenList,
                    PrIdnf = item.Idnf
                });
            }
            else
            {
                newItem = await dvExplrSvc.CreateTextFileAsync(
                    opts.PrIdnf, item.Name, item.Data.TextFileContents);

                OnItemCreated(item, newItem);
            }
        }

        private async Task MoveItemCoreAsync(
            DriveItemX item, bool move, bool rename)
        {
            var data = item.Data;
            string newItemName = item.Name;

            if (rename)
            {
                newItemName = data.RenameTo;
                data.IsRenamedFrom = item.Name;
            }

            if (move)
            {
                data.IsMovedFromPrIdnf = item.PrIdnf;

                if (item.IsFolder == true)
                {
                    await dvExplrSvc.MoveFolderAsync(item.Idnf,
                        data.MoveToPrIdnf, newItemName, false);
                }
                else
                {
                    await dvExplrSvc.MoveFileAsync(item.Idnf,
                        data.MoveToPrIdnf, newItemName);
                }
            }
            else if (rename)
            {
                if (item.IsFolder == true)
                {
                    await dvExplrSvc.RenameFolderAsync(
                        item.Idnf, data.RenameTo, false);
                }
                else
                {
                    await dvExplrSvc.RenameFileAsync(
                        item.Idnf, data.RenameTo);
                }
            }

            if (rename)
            {
                item.Name = data.RenameTo;
            }
        }

        private void OnItemCreated(
            DriveItemX inputItem,
            DriveItem newItem)
        {
            inputItem.Idnf ??= newItem.Idnf;
            inputItem.Data ??= new();

            inputItem.Data.IsCreated = true;
        }

        private List<DriveItemX> GetChildrenList(
            DriveItemX item)
        {
            List<DriveItemX> childrenList = new();

            AddChildrenIfReq(childrenList, item.SubFolders);
            AddChildrenIfReq(childrenList, item.FolderFiles);

            return childrenList;
        }

        private void AddChildrenIfReq(
            List<DriveItemX> retChildrenList,
            List<DriveItemX> childrenList)
        {
            if (childrenList != null)
            {
                retChildrenList.AddRange(childrenList);
            }
        }
    }
}
