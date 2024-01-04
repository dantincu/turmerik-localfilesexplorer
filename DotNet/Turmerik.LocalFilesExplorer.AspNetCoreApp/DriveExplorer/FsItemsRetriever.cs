using Turmerik.LocalFilesExplorer.AspNetCoreApp.Utility;

namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.DriveExplorer
{
    public interface IFsItemsRetriever : IDriveItemsRetriever
    {
        string RootDirPath { get; init; }
    }

    public class FsItemsRetriever : DriveItemsRetrieverBase, IFsItemsRetriever
    {
        protected readonly static string systemDrivePathRoot;
        protected readonly static string userProfilePath;
        protected readonly static string appDataDirName;
        protected readonly static string appDataPath;
        protected readonly static string appDataChildRelPathStartStr;

        protected readonly string rootDirPath;
        protected readonly bool hasRootDirPath;

        static FsItemsRetriever()
        {
            systemDrivePathRoot = Path.GetPathRoot(
                Environment.GetFolderPath(
                    Environment.SpecialFolder.System));

            userProfilePath = Environment.GetFolderPath(
                Environment.SpecialFolder.UserProfile);

            appDataDirName = Environment.GetFolderPath(
                Environment.SpecialFolder.ApplicationData).Substring(
                    userProfilePath.Length).Split(
                        Path.DirectorySeparatorChar.Arr(),
                        StringSplitOptions.RemoveEmptyEntries)[0];

            appDataPath = Path.Combine(
                userProfilePath,
                appDataDirName);

            appDataChildRelPathStartStr = appDataDirName + Path.DirectorySeparatorChar;
        }

        public FsItemsRetriever(ITimeStampHelper timeStampHelper) : base(timeStampHelper)
        {
            rootDirPath = string.Empty;
        }

        public bool AllowSysFolders { get; init; }

        public string RootDirPath
        {
            get => rootDirPath;

            init
            {
                if (!string.IsNullOrEmpty(value))
                {
                    hasRootDirPath = true;
                    rootDirPath = value;
                }
            }
        }

        public override async Task<DriveItem> GetItemAsync(
            string idnf, bool retMinimalInfo)
        {
            ThrowIfPathIsNotValidAgainstRootPath(idnf, true);
            DriveItem item;

            if (Directory.Exists(idnf))
            {
                item = await GetFolderAsync(
                    idnf, retMinimalInfo);
            }
            else if (File.Exists(idnf))
            {
                item = await GetFileAsync(idnf);
            }
            else
            {
                item = null;
            }

            return item;
        }

        public override async Task<DriveItem> GetFolderAsync(
            string idnf, bool retMinimalInfo)
        {
            ThrowIfPathIsNotValidAgainstRootPath(idnf, true);

            var folderPath = idnf;
            var entry = new DirectoryInfo(folderPath);
            var folder = GetDriveItem(entry);

            var driveItemsList = entry.EnumerateFileSystemInfos(
                ).Select(fi => GetDriveItem(fi)).ToList();

            folder.SubFolders = new List<DriveItem>(
                driveItemsList.Where(
                    item => item.IsFolder == true));

            folder.FolderFiles = new List<DriveItem>(
                driveItemsList.Where(
                    item => item.IsFolder != true).ToList());

            SortChildItems(folder);
            RemoveAdditionalInfoIfReq(folder, retMinimalInfo);

            return folder;
        }

        public override async Task<bool> ItemExistsAsync(
            string idnf) => await FileExistsAsync(
                idnf) || await FolderExistsAsync(idnf);

        public override async Task<bool> FolderExistsAsync(
            string idnf) => ThrowIfPathIsNotValidAgainstRootPath(idnf, true) && Directory.Exists(idnf);

        public override async Task<bool> FileExistsAsync(
            string idnf) => ThrowIfPathIsNotValidAgainstRootPath(idnf, false) && File.Exists(idnf);

        public override string GetItemIdnf<TDriveItem>(
            TDriveItem item,
            string prIdnf)
        {
            ThrowIfPathIsNotValidAgainstRootPath(prIdnf, true);

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

        public override async Task<string> GetFileTextAsync(string idnf)
        {
            ThrowIfPathIsNotValidAgainstRootPath(idnf, false);

            using var reader = new StreamReader(idnf);
            var text = await reader.ReadToEndAsync();

            return text;
        }

        public override Task<byte[]> GetFileBytesAsync(
            string idnf)
        {
            ThrowIfPathIsNotValidAgainstRootPath(idnf, false);

            var bytesArr = FsH.ReadAllBytesAsync(idnf);
            return bytesArr;
        }

        protected async Task<DriveItem> GetFileAsync(
            string idnf)
        {
            ThrowIfPathIsNotValidAgainstRootPath(idnf, false);

            var fSysInfo = new FileInfo(idnf);
            var item = GetDriveItem(fSysInfo);

            return item;
        }

        protected DriveItem GetDriveItem(
            FileSystemInfo fSysInfo)
        {
            var fsItem = new DriveItem
            {
                Name = fSysInfo.Name,
                Idnf = fSysInfo.FullName,
                CreationTime = fSysInfo.CreationTime,
                LastWriteTime = fSysInfo.LastWriteTime,
                LastAccessTime = fSysInfo.LastAccessTime,
            };

            if (fSysInfo is DirectoryInfo dirInfo)
            {
                fsItem.IsFolder = true;
            }
            else if (fSysInfo is FileInfo fInfo)
            {
                string extn = fSysInfo.Extension.ToLower();

                if (!string.IsNullOrEmpty(extn))
                {
                    if ((fsItem.FileType = GetFileType(extn)) == FileType.Document)
                    {
                        fsItem.OfficeFileType = GetOfficeFileType(extn);
                    }

                    if (PathH.CommonTextFileExtensions.Contains(extn))
                    {
                        fsItem.IsTextFile = true;
                    }
                    else if (PathH.CommonImageFileExtensions.Contains(extn))
                    {
                        fsItem.IsImageFile = true;
                    }
                    else if (PathH.CommonVideoFileExtensions.Contains(extn))
                    {
                        fsItem.IsVideoFile = true;
                    }
                    else if (PathH.CommonAudioFileExtensions.Contains(extn))
                    {
                        fsItem.IsAudioFile = true;
                    }
                }
            }

            return fsItem;
        }

        protected override char GetDirSeparator() => Path.DirectorySeparatorChar;

        protected override void RemoveAdditionalInfoIfReq(
            DriveItem item, bool retMinimalInfo)
        {
            base.RemoveAdditionalInfoIfReq(item, retMinimalInfo);

            if (retMinimalInfo)
            {
                item.Idnf = null;
            }
        }

        protected bool ThrowIfPathIsNotValidAgainstRootPath(
            string path, bool allowsRootPath)
        {
            bool isValid = PathIsValidAgainstRootPath(path, allowsRootPath);

            if (!isValid)
            {
                if (hasRootDirPath)
                {
                    throw new DriveExplorerException(
                        $"All paths are required to fall under root path `{rootDirPath}`");
                }
                else
                {
                    throw new DriveExplorerException(
                        string.Join(" ", $"All paths are required to either have a different root than the system root or fall under user profile path `{userProfilePath}`",
                            $"as a nested folder that does not start with the dot char '.' and is not equal to the app data dir name `{appDataDirName}`"));
                }
            }

            return isValid;
        }

        protected bool PathIsValidAgainstRootPath(
            string path, bool allowsRootPath)
        {
            bool canBeValid = path != null && path == path.Trim() && Path.IsPathRooted(
                path) && !path.Contains(
                "..") && !path.ContainsAny(
                    PathH.InvalidPathCharsStr) && !path.EndsWith(":");

            bool isValid = canBeValid;

            if (isValid)
            {
                isValid = !hasRootDirPath;
            }

            if (canBeValid && !isValid)
            {
                isValid = IsChildPathOf(
                    rootDirPath, path,
                    allowsRootPath, out _);
            }

            if (isValid && !AllowSysFolders)
            {
                if (Path.GetPathRoot(path) == systemDrivePathRoot)
                {
                    isValid = IsChildPathOf(
                        userProfilePath, path, false,
                        out string? restOfPath);

                    isValid = isValid && restOfPath.First() != '.' &&
                        restOfPath != appDataDirName && !restOfPath.StartsWith(
                            appDataChildRelPathStartStr);
                }
            }

            return isValid;
        }

        protected bool IsChildPathOf(
            string basePath,
            string trgPath,
            bool allowsEqualToBasePath,
            out string? restOfPath)
        {
            restOfPath = null;
            bool isChildOf = trgPath.StartsWith(basePath);

            if (isChildOf)
            {
                string restOfPathStr = trgPath.Substring(
                    basePath.Length);

                string trimmedRestOfPath = PathTrimStart(
                    restOfPathStr);

                bool restOfPathIsEmpty = string.IsNullOrWhiteSpace(
                    trimmedRestOfPath);

                if (restOfPathIsEmpty)
                {
                    isChildOf = allowsEqualToBasePath;
                }
                else
                {
                    isChildOf = restOfPathStr.First() == Path.DirectorySeparatorChar;
                }

                if (isChildOf)
                {
                    restOfPath = trimmedRestOfPath;
                }
            }

            return isChildOf;
        }

        private string PathTrimStart(
            string path) => path.TrimStart(
            Path.DirectorySeparatorChar,
            Path.AltDirectorySeparatorChar,
            '.', ' ');
    }
}
