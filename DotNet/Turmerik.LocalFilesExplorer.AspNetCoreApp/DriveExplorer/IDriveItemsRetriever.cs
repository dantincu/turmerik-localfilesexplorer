using System.Collections.ObjectModel;
using Turmerik.LocalFilesExplorer.AspNetCoreApp.Utility;

namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.DriveExplorer
{
    public interface IDriveItemsRetriever
    {
        Task<DriveItem> GetItemAsync(
            string idnf, bool retMinimalInfo);

        Task<DriveItem> GetFolderAsync(
            string idnf, bool retMinimalInfo);

        Task<DriveItem> GetFolderAsync(
            string idnf, int depth, bool retMinimalInfo);

        Task<bool> ItemExistsAsync(string idnf);

        Task<bool> FolderExistsAsync(string idnf);

        Task<bool> FileExistsAsync(string idnf);

        Task<string> GetFileTextAsync(string idnf);

        Task<byte[]> GetFileBytesAsync(string idnf);

        string GetItemIdnf<TDriveItem>(
            TDriveItem item,
            string prIdnf)
            where TDriveItem : DriveItem<TDriveItem>;

        char DirSeparator { get; }
    }

    public abstract class DriveItemsRetrieverCoreBase : IDriveItemsRetriever
    {
        protected DriveItemsRetrieverCoreBase()
        {
            DirSeparator = GetDirSeparator();
        }

        public char DirSeparator { get; }

        public abstract Task<bool> ItemExistsAsync(string idnf);
        public abstract Task<bool> FileExistsAsync(string idnf);
        public abstract Task<bool> FolderExistsAsync(string idnf);

        public abstract Task<DriveItem> GetItemAsync(
            string idnf, bool retMinimalInfo);

        public abstract Task<DriveItem> GetFolderAsync(
            string idnf, bool retMinimalInfo);

        public abstract Task<string> GetFileTextAsync(string idnf);
        public abstract Task<byte[]> GetFileBytesAsync(string idnf);

        public async Task<DriveItem> GetFolderAsync(
            string idnf, int depth, bool retMinimalInfo)
        {
            var folder = await GetFolderAsync(idnf, retMinimalInfo);
            string folderPath = idnf;
            var subFolders = folder.SubFolders;

            if (subFolders != null && depth > 0)
            {
                int count = subFolders.Count;

                for (int i = 0; i < count; i++)
                {
                    string childIdnf = Path.Combine(
                        folderPath,
                        subFolders[i].Name);

                    subFolders[i] = await GetFolderAsync(
                        childIdnf, depth - 1, retMinimalInfo);
                }
            }

            return folder;
        }

        public abstract string GetItemIdnf<TDriveItem>(
            TDriveItem item,
            string prIdnf)
            where TDriveItem : DriveItem<TDriveItem>;

        protected abstract char GetDirSeparator();

        protected virtual void RemoveAdditionalInfoIfReq(
            DriveItem item, bool retMinimalInfo)
        {
            if (retMinimalInfo)
            {
                item.PrIdnf = null;
                item.IsFolder = null;
                item.IsRootFolder = null;

                item.OfficeFileType = null;
                item.FileType = null;

                item.IsTextFile = null;
                item.IsImageFile = null;
                item.IsVideoFile = null;
                item.IsAudioFile = null;

                if (item.SubFolders != null)
                {
                    foreach (var folder in item.SubFolders)
                    {
                        RemoveAdditionalInfoIfReq(
                            folder, retMinimalInfo);
                    }
                }

                if (item.FolderFiles != null)
                {
                    foreach (var file in item.FolderFiles)
                    {
                        RemoveAdditionalInfoIfReq(
                            file, retMinimalInfo);
                    }
                }
            }
        }
    }

    public abstract class DriveItemsRetrieverBase : DriveItemsRetrieverCoreBase
    {
        protected static readonly ReadOnlyDictionary<OfficeFileType, ReadOnlyCollection<string>> OfficeFileTypesFileNameExtensions;
        protected static readonly ReadOnlyDictionary<FileType, ReadOnlyCollection<string>> FileTypesFileNameExtensions;

        static DriveItemsRetrieverBase()
        {
            OfficeFileTypesFileNameExtensions = new Dictionary<OfficeFileType, ReadOnlyCollection<string>>
            {
                { OfficeFileType.Word, new string[] { ".docx", ".doc" }.RdnlC() },
                { OfficeFileType.Excel, new string[] { ".xlsx", ".xls" }.RdnlC() },
                { OfficeFileType.PowerPoint, new string[] { ".pptx" , ".ppt" }.RdnlC() },
            }.RdnlD();

            FileTypesFileNameExtensions = new Dictionary<FileType, ReadOnlyCollection<string>>
            {
                { FileType.PlainText, new string [] { ".txt", ".md", ".log", ".logs" }.RdnlC() },
                { FileType.Document, OfficeFileTypesFileNameExtensions.Values.SelectMany(
                    arr => arr).Concat(new string[] { ".pdf", ".rtf" }).RdnlC() },
                { FileType.Image, new string[] { ".jpg", ".jpeg", ".png", ".giff", ".tiff", ".img", ".ico", ".bmp", ".heic" }.RdnlC() },
                { FileType.Audio, new string[] { ".mp3", ".flac", ".wav", ".aac" }.RdnlC() },
                { FileType.Video, new string[] { ".mpg", ".mpeg", ".avi", ".mp4", ".m4a" }.RdnlC() },
                { FileType.Code, new string[] { ".cs", "vb", ".js", ".ts", ".json", ".jsx", ".tsx", ".csx", ".vbx",
                    ".csproj", ".vbproj", ".sln", ".xml", ".yml", ".xaml", ".yaml", ".toml", ".html", ".cshtml", ".vbhtml",
                    ".c", ".h", ".cpp", ".java", ".config", ".cfg", ".ini" }.RdnlC() },
                { FileType.Binary, new string[] { ".bin", ".exe", ".lib", ".jar" }.RdnlC() },
                { FileType.ZippedFolder, new string[] { ".zip", ".rar", ".tar", ".7z" }.RdnlC() },
            }.RdnlD();
        }

        public DriveItemsRetrieverBase(
            ITimeStampHelper timeStampHelper)
        {
            TimeStampHelper = timeStampHelper ?? throw new ArgumentNullException(nameof(timeStampHelper));
        }

        protected ITimeStampHelper TimeStampHelper { get; }

        protected string GetTimeStampStr(DateTime? dateTime)
        {
            string timeStampStr = null;

            if (dateTime.HasValue)
            {
                DateTime dateTimeValue = dateTime.Value;

                timeStampStr = TimeStampHelper.TmStmp(
                    dateTimeValue,
                    true,
                    TimeStamp.Seconds);
            }

            return timeStampStr;
        }

        protected FileType? GetFileType(string extn)
        {
            var matchKvp = FileTypesFileNameExtensions.SingleOrDefault(
                kvp => kvp.Value.Contains(extn));

            FileType? retVal = null;

            if (matchKvp.Value != null)
            {
                retVal = matchKvp.Key;
            }

            return retVal;
        }

        protected OfficeFileType? GetOfficeFileType(string extn)
        {
            var matchKvp = OfficeFileTypesFileNameExtensions.SingleOrDefault(
                kvp => kvp.Value.Contains(extn));

            OfficeFileType? retVal = null;

            if (matchKvp.Value != null)
            {
                retVal = matchKvp.Key;
            }

            return retVal;
        }

        protected void SortChildItems(DriveItem folder)
        {
            folder.SubFolders.Sort((f1, f2) => f1.Name.CompareTo(f2.Name));
            folder.FolderFiles.Sort((f1, f2) => f1.Name.CompareTo(f2.Name));
        }
    }
}
