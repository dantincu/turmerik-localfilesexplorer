using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using Turmerik.LocalFilesExplorer.AspNetCoreApp.DriveExplorer;

namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LocalFilesExplorerController : ControllerBase
    {
        private readonly IDriveExplorerService dvExplrSvc;

        public LocalFilesExplorerController(
            IDriveExplorerService dvExplrSvc)
        {
            this.dvExplrSvc = dvExplrSvc ?? throw new ArgumentNullException(nameof(dvExplrSvc));
        }

        [HttpGet]
        public Task<DriveItem> GetItemAsync(
            string idnf) => TryGetAsync(() => dvExplrSvc.GetItemAsync(idnf, true));

        [HttpGet]
        public Task<DriveItem> GetFolderAsync(
            string idnf) => TryGetAsync(() => dvExplrSvc.GetFolderAsync(idnf, true));

        [HttpGet]
        public Task<bool> ItemExistsAsync(
            string idnf) => TryGetAsync(() => dvExplrSvc.ItemExistsAsync(idnf));

        [HttpGet]
        public Task<bool> FolderExistsAsync(
            string idnf) => TryGetAsync(() => dvExplrSvc.FolderExistsAsync(idnf));

        [HttpGet]
        public Task<bool> FileExistsAsync(
            string idnf) => TryGetAsync(() => dvExplrSvc.FileExistsAsync(idnf));

        [HttpGet]
        public Task<string> GetFileTextAsync(
            string idnf) => TryGetAsync(() => dvExplrSvc.GetFileTextAsync(idnf));

        [HttpGet]
        public Task<string> GetDriveFolderWebUrlAsync(
            string idnf) => TryGetAsync(() => dvExplrSvc.GetDriveFolderWebUrlAsync(
                idnf));

        [HttpGet]
        public Task<string> GetDriveFileWebUrlAsync(
            string idnf) => TryGetAsync(() => dvExplrSvc.GetDriveFileWebUrlAsync(
                idnf));

        [HttpPost]
        public Task<DriveItem> CreateFolderAsync(
            string prIdnf, string newFolderName) => TryGetAsync(() => dvExplrSvc.CreateFolderAsync(
                prIdnf, newFolderName, true));

        [HttpPost]
        public Task<DriveItem> RenameFolderAsync(
            string idnf, string newFolderName) => TryGetAsync(() => dvExplrSvc.RenameFolderAsync(
                idnf, newFolderName, true));

        [HttpPost]
        public Task<DriveItem> CopyFolderAsync(
            string idnf, string newPrIdnf, string newFolderName) => TryGetAsync(() => dvExplrSvc.CopyFolderAsync(
                idnf, newPrIdnf, newFolderName, true));

        [HttpPost]
        public Task<DriveItem> MoveFolderAsync(
            string idnf, string newPrIdnf, string newFolderName) => TryGetAsync(() => dvExplrSvc.MoveFolderAsync(
                idnf, newPrIdnf, newFolderName, true));

        [HttpPost]
        public Task<DriveItem> DeleteFolderAsync(
            string idnf) => TryGetAsync(() => dvExplrSvc.DeleteFolderAsync(idnf, true));

        [HttpPost]
        public Task<DriveItem> CreateTextFileAsync(
            string prIdnf, string newFileName, string text) => TryGetAsync(() => dvExplrSvc.CreateTextFileAsync(
                prIdnf, newFileName, text));

        [HttpPost]
        public Task<DriveItem> CreateOfficeLikeFileAsync(
            string prIdnf,
            string newFileName,
            OfficeFileType officeLikeFileType) => TryGetAsync(() => dvExplrSvc.CreateOfficeLikeFileAsync(
                prIdnf, newFileName, officeLikeFileType));

        [HttpPost]
        public Task<DriveItem> RenameFileAsync(
            string idnf, string newFileName) => TryGetAsync(() => dvExplrSvc.RenameFileAsync(
                idnf, newFileName));

        [HttpPost]
        public Task<DriveItem> CopyFileAsync(
            string idnf, string newPrIdnf, string newFileName) => TryGetAsync(() => dvExplrSvc.CopyFileAsync(
                idnf, newPrIdnf, newFileName));

        [HttpPost]
        public Task<DriveItem> MoveFileAsync(
            string idnf, string newPrIdnf, string newFileName) => TryGetAsync(() => dvExplrSvc.MoveFileAsync(
                idnf, newPrIdnf, newFileName));

        [HttpPost]
        public Task<DriveItem> DeleteFileAsync(
            string idnf) => TryGetAsync(() => dvExplrSvc.DeleteFileAsync(idnf));

        private async Task<TResult> TryGetAsync<TResult>(
            Func<Task<TResult>> action)
        {
            TResult result = default;

            try
            {
                result = await action();
            }
            catch (DirectoryNotFoundException ex)
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
            }
            catch (FileNotFoundException ex)
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
            }
            catch
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }

            return result;
        }
    }
}
