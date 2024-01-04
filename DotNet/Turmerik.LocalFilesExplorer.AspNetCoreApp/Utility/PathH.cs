using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

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
    }
}
