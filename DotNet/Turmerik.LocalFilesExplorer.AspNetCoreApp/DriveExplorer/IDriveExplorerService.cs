using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.DriveExplorer
{
    public interface IDriveExplorerService : IDriveItemsRetriever
    {
        Task<string> GetDriveFolderWebUrlAsync(string idnf);
        Task<string> GetDriveFileWebUrlAsync(string idnf);

        Task<DriveItem> CreateFolderAsync(
            string prIdnf,
            string newFolderName,
            bool retMinimalInfo);

        Task<DriveItem> RenameFolderAsync(
            string idnf,
            string newFolderName,
            bool retMinimalInfo);

        Task<DriveItem> CopyFolderAsync(
            string idnf,
            string newPrIdnf,
            string newFolderName,
            bool retMinimalInfo);

        Task<DriveItem> MoveFolderAsync(
            string idnf,
            string newPrIdnf,
            string newFolderName,
            bool retMinimalInfo);

        Task<DriveItem> DeleteFolderAsync(
            string idnf, bool retMinimalInfo);

        Task<DriveItem> CreateTextFileAsync(
            string prIdnf, string newFileName, string text);

        Task<DriveItem> CreateOfficeLikeFileAsync(
            string prIdnf,
            string newFileName,
            OfficeFileType officeLikeFileType);

        Task<DriveItem> RenameFileAsync(
            string idnf, string newFileName);

        Task<DriveItem> CopyFileAsync(
            string idnf, string newPrIdnf, string newFileName);

        Task<DriveItem> MoveFileAsync(
            string idnf, string newPrIdnf, string newFileName);

        Task<DriveItem> DeleteFileAsync(
            string idnf);
    }
}
