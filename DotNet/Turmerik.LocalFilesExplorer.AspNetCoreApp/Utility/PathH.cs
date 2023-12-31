using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Turmerik.LocalFilesExplorer.AspNetCoreApp.Helpers;
using Turmerik.LocalFilesExplorer.AspNetCoreApp.Utility;

namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.Utility
{
    public static class PathH
    {
        public const string FILE_URI_SCHEME = "file://";

        public static readonly ReadOnlyCollection<char> InvalidPathChars;
        public static readonly ReadOnlyCollection<char> InvalidFileNameChars;
        public static readonly ReadOnlyCollection<char> InvalidFileNameCharsExclSep;
        public static readonly ReadOnlyCollection<char> DirSeparatorChars;

        public static readonly string InvalidPathCharsStr;
        public static readonly string InvalidFileNameCharsStr;
        public static readonly string InvalidFileNameCharsExclSepStr;

        public static readonly ReadOnlyCollection<string> CommonTextFileExtensions = new string[]
        {
            ".txt", ".md", ".c", ".cpp", ".cs", ".java", ".xml", ".html", ".css", ".js", ".ts", ".json", ".scss", ".less", ".jsx", ".tsx"
        }.RdnlC();

        public static readonly ReadOnlyCollection<string> CommonImageFileExtensions = new string[]
        {
            ".jpg", ".jpeg", ".gif", ".png", ".bmp", ".ico"
        }.RdnlC();

        public static readonly ReadOnlyCollection<string> CommonVideoFileExtensions = new string[]
        {
            ".avi", ".mpeg", ".mpg", ".mp4", ".m4a", ".ogg"
        }.RdnlC();

        public static readonly ReadOnlyCollection<string> CommonAudioFileExtensions = new string[]
        {
            ".mp3", ".flac", ".aac", ".wav"
        }.RdnlC();

        public static readonly string DirSepChar = Path.DirectorySeparatorChar.ToString();
        public static readonly string AltDirSepChar = Path.AltDirectorySeparatorChar.ToString();

        public static readonly string ParentDir = $"..{Path.DirectorySeparatorChar}";
        public static readonly string AltParentDir = $"..{Path.AltDirectorySeparatorChar}";

        public static readonly string NetworkPathRootPfx = StringH.JoinStrRange(2, DirSepChar);
        public static readonly string NetworkPathRootAltPfx = StringH.JoinStrRange(2, AltDirSepChar);

        public static readonly Regex WinDriveRegex = new Regex(@"^[a-zA-Z]\:");

        static PathH()
        {
            var invalidPathChars = Path.GetInvalidPathChars();
            var invalidFileNameChars = Path.GetInvalidFileNameChars();
            var invalidFileNameCharsExcSep = invalidFileNameChars.Except([Path.DirectorySeparatorChar]).ToArray();

            InvalidPathChars = invalidPathChars.RdnlC();
            InvalidFileNameChars = invalidFileNameChars.RdnlC();
            InvalidFileNameCharsExclSep = invalidFileNameCharsExcSep.RdnlC();
            DirSeparatorChars = new char[] { '\\', '/' }.RdnlC();

            InvalidPathCharsStr = invalidPathChars.ToStr();
            InvalidFileNameCharsStr = invalidFileNameChars.ToStr();
            InvalidFileNameCharsExclSepStr = invalidFileNameCharsExcSep.ToStr();
        }

        public static string CombinePaths(
            string[] pathParts,
            string dirSep)
        {
            pathParts = pathParts.Select(
                part => part?.Trim().Trim('/', '\\')).ToArray();

            pathParts = pathParts.Where(
                part => !string.IsNullOrWhiteSpace(part)).ToArray();

            string retPath = null;

            if (pathParts.Any())
            {
                retPath = string.Join(dirSep, pathParts);
            }

            if (retPath?.EndsWith(":") ?? false)
            {
                retPath += dirSep;
            }

            return retPath;
        }

        public static bool ContainsInvalidPathChars(
            string path,
            bool isFileName = false,
            bool throwIfInvalid = false)
        {
            var invalidPathChars = GetInvalidPathChars(isFileName);
            bool retVal = path.ContainsAny(invalidPathChars);

            if (!retVal && throwIfInvalid)
            {
                char[] contained = path.GetContained(invalidPathChars);
                string containedStr = string.Join(", ", contained);

                throw new ArgumentException(
                    $"Provided path {path} contains the following invalid path chars: {containedStr}");
            }

            return retVal;
        }

        public static string ReplaceInvalidPathChars(
            string path,
            bool isFileName = false,
            Func<char, char> replaceFactory = null)
        {
            var invalidPathChars = GetInvalidPathChars(isFileName);

            string retPath = path.ReplaceChars(
                replaceFactory,
                invalidPathChars);

            return retPath;
        }

        public static ReadOnlyCollection<char> GetInvalidPathChars(
            bool isFileName)
        {
            ReadOnlyCollection<char> invalidPathChars;

            if (isFileName)
            {
                invalidPathChars = InvalidPathChars;
            }
            else
            {
                invalidPathChars = InvalidFileNameChars;
            }

            return invalidPathChars;
        }

        public static string GetDirName(string path)
        {
            path = path?.TrimEnd('/', '\\') ?? string.Empty;
            string dirName = null;

            if (path.LastOrDefault() == ':')
            {
                dirName = path ?? string.Empty;
            }
            else
            {
                dirName = Path.GetFileName(path);
            }

            return dirName;
        }

        public static string GetDirPath(string path)
        {
            path = path?.TrimEnd('/', '\\') ?? string.Empty;
            string dirPath = string.Empty;

            if (path.LastOrDefault() != ':')
            {
                dirPath = Path.GetDirectoryName(path);
            }

            return dirPath;
        }

        public static TResult WithPath<TResult>(
            string path,
            Func<string, TResult> ifNotRooted,
            Func<string, TResult> ifRooted)
        {
            TResult result;

            if (Path.IsPathRooted(path))
            {
                result = ifRooted(path);
            }
            else
            {
                result = ifNotRooted(path);
            }

            return result;
        }

        public static string AssurePathRooted(
            string path,
            Func<string> defaultBasePathFactory,
            Func<string, string> normalizer = null) => WithPath(
                path,
                ph =>
                {
                    string defaultBasePath = defaultBasePathFactory();

                    string retPath = Path.Combine(
                        defaultBasePath, ph);

                    if (normalizer != null)
                    {
                        retPath = normalizer(retPath);
                    }

                    return retPath;
                },
                ph => ph);

        public static string AssurePathRooted(
            string path,
            string defaultBasePath,
            Func<string, string> normalizer) => AssurePathRooted(
                path, () => defaultBasePath, normalizer);

        public static string AssurePathRooted(
            string path,
            string defaultBasePath) => AssurePathRooted(
                path, defaultBasePath, Path.GetFullPath);
    }
}
