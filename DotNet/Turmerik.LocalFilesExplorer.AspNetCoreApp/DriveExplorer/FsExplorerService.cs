using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turmerik.LocalFilesExplorer.AspNetCoreApp.Utility;

namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.DriveExplorer
{
    public interface IFsExplorerService : IFsItemsRetriever, IDriveExplorerService
    {
    }

    public class FsExplorerService : FsItemsRetriever, IFsExplorerService
    {
        public FsExplorerService(
            ITimeStampHelper timeStampHelper) : base(timeStampHelper)
        {
        }

        public async Task<DriveItem> CopyFileAsync(
            string idnf,
            string newPrIdnf,
            string newFileName)
        {
            ThrowIfPathIsNotValidAgainstRootPath(idnf, false);
            ThrowIfPathIsNotValidAgainstRootPath(newPrIdnf, true);

            string newPath = Path.Combine(
                newPrIdnf, newFileName);

            File.Copy(idnf, newPath);

            var newEntry = new FileInfo(newPath);
            var item = GetDriveItem(newEntry);

            return item;
        }

        public async Task<DriveItem> CopyFolderAsync(
            string idnf,
            string newPrIdnf,
            string newFolderName,
            bool retMinimalInfo)
        {
            ThrowIfPathIsNotValidAgainstRootPath(idnf, false);
            ThrowIfPathIsNotValidAgainstRootPath(newPrIdnf, true);

            string newPath = Path.Combine(
                newPrIdnf, newFolderName);

            FsH.CopyDirectory(
                idnf,
                newPath);

            var newEntry = new DirectoryInfo(newPath);
            var item = GetDriveItem(newEntry);

            RemoveAdditionalInfoIfReq(item, retMinimalInfo);
            return item;
        }

        public async Task<DriveItem> CreateFolderAsync(
            string prIdnf,
            string newFolderName,
            bool retMinimalInfo)
        {
            ThrowIfPathIsNotValidAgainstRootPath(prIdnf, true);

            string newPath = Path.Combine(
                prIdnf, newFolderName);

            Directory.CreateDirectory(newPath);

            var newEntry = new DirectoryInfo(newPath);
            var item = GetDriveItem(newEntry);

            RemoveAdditionalInfoIfReq(item, retMinimalInfo);
            return item;
        }

        public async Task<DriveItem> CreateOfficeLikeFileAsync(
            string prIdnf,
            string newFileName,
            OfficeFileType officeLikeFileType)
        {
            var result = await CreateTextFileAsync(
                prIdnf,
                newFileName,
                string.Empty);

            return result;
        }

        public async Task<DriveItem> CreateTextFileAsync(
            string prIdnf,
            string newFileName,
            string text)
        {
            ThrowIfPathIsNotValidAgainstRootPath(prIdnf, false);

            string newPath = Path.Combine(
                prIdnf, newFileName);

            File.WriteAllText(newPath, text);

            var newEntry = new FileInfo(newPath);
            var item = GetDriveItem(newEntry);

            return item;
        }

        public async Task<DriveItem> DeleteFileAsync(string idnf)
        {
            ThrowIfPathIsNotValidAgainstRootPath(idnf, false);

            var fileInfo = new FileInfo(idnf);
            var driveItem = GetDriveItem(fileInfo);

            fileInfo.Delete();
            return driveItem;
        }

        public async Task<DriveItem> DeleteFolderAsync(
            string idnf,
            bool retMinimalInfo)
        {
            ThrowIfPathIsNotValidAgainstRootPath(idnf, false);

            var dirInfo = new DirectoryInfo(idnf);
            var driveItem = GetDriveItem(dirInfo);

            dirInfo.Delete(true);

            SortChildItems(driveItem);
            RemoveAdditionalInfoIfReq(driveItem, retMinimalInfo);

            return driveItem;
        }

        public async Task<string> GetDriveFolderWebUrlAsync(
            string idnf) => GetDriveItemUrl(idnf);

        public async Task<string> GetDriveFileWebUrlAsync(
            string idnf) => GetDriveItemUrl(idnf);

        public async Task<DriveItem> MoveFileAsync(
            string idnf,
            string newPrIdnf,
            string newFileName)
        {
            ThrowIfPathIsNotValidAgainstRootPath(idnf, false);
            ThrowIfPathIsNotValidAgainstRootPath(newPrIdnf, true);

            string path = idnf;
            string newPath = Path.Combine(path, newFileName);

            File.Move(path, newPath);
            var newEntry = new FileInfo(newPath);

            var item = GetDriveItem(newEntry);
            return item;
        }

        public async Task<DriveItem> MoveFolderAsync(
            string idnf,
            string newPrIdnf,
            string newFolderName,
            bool retMinimalInfo)
        {
            ThrowIfPathIsNotValidAgainstRootPath(idnf, false);
            ThrowIfPathIsNotValidAgainstRootPath(newPrIdnf, true);

            string path = idnf;
            string newPrPath = newPrIdnf;

            string newPath = Path.Combine(
                newPrPath, newFolderName);

            FsH.MoveDirectory(path, newPath);

            var newEntry = new DirectoryInfo(newPath);
            var item = GetDriveItem(newEntry);

            SortChildItems(item);
            RemoveAdditionalInfoIfReq(item, retMinimalInfo);

            return item;
        }

        public async Task<DriveItem> RenameFileAsync(
            string idnf,
            string newFileName)
        {
            string parentIdnf = Path.GetDirectoryName(idnf);
            ThrowIfPathIsNotValidAgainstRootPath(parentIdnf, true);

            var result = await MoveFileAsync(
                idnf, parentIdnf, newFileName);

            return result;
        }

        public async Task<DriveItem> RenameFolderAsync(
            string idnf,
            string newFolderName,
            bool retMinimalInfo)
        {
            string parentIdnf = Path.GetDirectoryName(idnf);
            ThrowIfPathIsNotValidAgainstRootPath(parentIdnf, true);

            var result = await MoveFolderAsync(
                idnf, parentIdnf, newFolderName, retMinimalInfo);

            return result;
        }

        private string GetDriveItemUrl(
            string path) => $"file://{path}";
    }
}
