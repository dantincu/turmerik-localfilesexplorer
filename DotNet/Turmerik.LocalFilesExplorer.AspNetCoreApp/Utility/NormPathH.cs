using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Turmerik.LocalFilesExplorer.AspNetCoreApp.Helpers;

namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.Utility
{
    public static class NormPathH
    {
        public static string LocalDeviceRootedPathUnixToWinStyle(
            string path,
            Func<string, string>? transformer = null) => string.Concat(
                path[1], ':', path.Substring(2)).With(NormalizeTransformer(transformer));

        public static string RootedPathUnixToWinStyle(
            string path,
            Func<string, string>? transformer = null) => path.StartsWith(PathH.NetworkPathRootPfx) switch
            {
                false => LocalDeviceRootedPathUnixToWinStyle(path, transformer),
                true => path.With(NormalizeTransformer(transformer))
            };

        public static string NormRootedPathWinStyle(
            string path,
            Func<string, string>? transformer = null) => PathH.WinDriveRegex.IsMatch(path) switch
            {
                false => NormPathCore(path).With(retPath => RootedPathUnixToWinStyle(retPath, transformer)),
                true => Path.GetFullPath(path).With(NormalizeTransformer(transformer))
            };

        public static string NormPathUnixStyle(
            string path,
            Func<string, bool, string>? transformer = null)
        {
            path = NormPath(path);
            path = path.Replace("\\", "/");
            bool isRootedPath;

            if (PathH.WinDriveRegex.IsMatch(path))
            {
                isRootedPath = true;

                (var drive,
                    var restOfPath) = path.SplitStr(
                        (str, len) => 2);

                path = string.Concat(
                    '/', drive[0], restOfPath);
            }
            else
            {
                isRootedPath = path.FirstOrDefault() == '/';
            }

            if (transformer != null)
            {
                path = transformer(path, isRootedPath);
            }

            return path;
        }

        public static string NormRootedPath(
            string path,
            Func<string, string>? transformer = null) => LocalDeviceH.IsWinOS switch
            {
                false => Path.GetFullPath(path).With(NormalizeTransformer(transformer)),
                true => NormRootedPathWinStyle(path, transformer)
            };

        public static string NormPath(
            string path,
            bool forceUnixStyle,
            Func<string, bool, string>? transformer = null) => forceUnixStyle switch
            {
                false => NormPath(path, transformer),
                true => NormPathUnixStyle(path, transformer),
            };

        public static string NormPath(
            string path,
            Func<string, bool, string>? transformer = null) => Path.IsPathRooted(path) switch
            {
                false => NormPathCore(path, ConvertTransformer(transformer, false)),
                true => NormRootedPath(path, ConvertTransformer(transformer, true))
            };

        /// <summary>
        /// StrPrnPnt stands for "starting parent pointers"
        /// </summary>
        /// <param name="path"></param>
        /// <param name="strPrnPntCount"></param>
        /// <returns></returns>
        public static string NormPathCore(
            string path,
            Func<string, string>? transformer = null)
        {
            var partsArr = TrimAndSplitByDirSepChars(path);

            var partsList = partsArr.Where(
                part => part != ".").ToList();

            int strPrnPntCount = RemPrnPnt(partsList);

            if (strPrnPntCount > 0)
            {
                var stPrPnArr = Enumerable.Range(
                    0, strPrnPntCount).Select(
                        idx => "..").ToArray();

                partsList.InsertRange(0, stPrPnArr);
            }

            string retPath = partsList.ToArray(
                ).JoinNotNullStr(
                PathH.DirSepChar, false);

            retPath = retPath.TrimEnd('/', '\\');

            if (transformer != null)
            {
                retPath = transformer(retPath);
            }

            return retPath;
        }

        public static int RemPrnPnt(
            List<string> partsList)
        {
            int i = 0;
            int startingPointersToParent = 0;
            int count = partsList.Count;

            while (i < count)
            {
                var part = partsList[i];

                if (part == "..")
                {
                    if (i > startingPointersToParent)
                    {
                        partsList.RemoveRange(i - 1, 2);
                        count -= 2;
                    }
                    else
                    {
                        i++;
                        startingPointersToParent++;
                    }
                }
                else if (part.LastOrDefault() == '.')
                {
                    ThrowInvalidPathEntryName(part);
                }
                else
                {
                    i++;
                }
            }

            partsList.RemoveRange(0, startingPointersToParent);
            return startingPointersToParent;
        }

        public static string[] TrimAndSplitByDirSepChars(
            string path) => path.Split(
                Path.DirectorySeparatorChar,
                Path.AltDirectorySeparatorChar).Select(
                part => part.Trim()).ToArray();

        public static void ThrowInvalidPathEntryName(
            string pathPart) => throw new FormatException(
                $"Invalid path entry name: {pathPart}");

        public static Func<string, string> ConvertTransformer(
            Func<string, bool, string>? transformer,
            bool isPathRooted) => path => NormalizeTransformer(transformer).Invoke(path, isPathRooted);

        public static Func<string, string> NormalizeTransformer(
            Func<string, string>? transformer) => transformer.FirstNotNull(path => path)!;

        public static Func<string, bool, string> NormalizeTransformer(
            Func<string, bool, string>? transformer) => transformer.FirstNotNull((path, isRooted) => path)!;
    }
}
