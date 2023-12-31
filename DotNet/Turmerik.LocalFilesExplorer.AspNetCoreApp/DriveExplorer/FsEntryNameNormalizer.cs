using Turmerik.LocalFilesExplorer.AspNetCoreApp.Utility;

namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.DriveExplorer
{
    public interface IFsEntryNameNormalizer
    {
        string NormalizeFsEntryName(
            string title,
            int maxLength,
            bool noTwoConsecutiveSpaces = true);
    }

    public class FsEntryNameNormalizer : IFsEntryNameNormalizer
    {
        public string NormalizeFsEntryName(
            string title,
            int maxLength,
            bool noTwoConsecutiveSpaces = true)
        {
            var charsList = GetCharsList(
                title, noTwoConsecutiveSpaces);

            NormalizeCharsList(
                charsList,
                maxLength);

            return new string(
                charsList.ToArray());
        }

        private List<char> GetCharsList(
            string title,
            bool noTwoConsecutiveSpaces)
        {
            List<char> charsList = new();
            int strLen = title.Length;
            bool lastAddedWasSpace = false;

            for (int i = 0; i < strLen; i++)
            {
                char chrToAdd = GetCharToAdd(
                    title[i], ref lastAddedWasSpace,
                    noTwoConsecutiveSpaces);

                if (chrToAdd != default)
                {
                    charsList.Add(chrToAdd);
                }
            }

            return charsList;
        }

        private char GetCharToAdd(
            char chr,
            ref bool lastAddedWasSpace,
            bool noTwoConsecutiveSpaces)
        {
            char chrToAdd = GetCharToAdd(chr);

            chrToAdd = HandleConsecutiveSpacesIfReq(
                chrToAdd, ref lastAddedWasSpace,
                noTwoConsecutiveSpaces);

            return chrToAdd;
        }

        private char GetCharToAdd(
            char chr)
        {
            char chrToAdd = chr;

            if (PathH.InvalidFileNameChars.Contains(chr))
            {
                chrToAdd = HandleInvalidFileNameChars(chr);
            }
            else if (chr != ' ' && char.IsWhiteSpace(chr))
            {
                chrToAdd = ' ';
            }
            else if (char.IsControl(chr))
            {
                chrToAdd = default;
            }

            return chrToAdd;
        }

        private char HandleInvalidFileNameChars(
            char chr) => chr switch
            {
                '/' => '%',
                _ => default
            };

        private char HandleConsecutiveSpacesIfReq(
            char chrToAdd,
            ref bool lastAddedWasSpace,
            bool noTwoConsecutiveSpaces)
        {
            if (noTwoConsecutiveSpaces && chrToAdd != default)
            {
                if (chrToAdd == ' ')
                {
                    if (lastAddedWasSpace)
                    {
                        chrToAdd = default;
                    }
                    else
                    {
                        lastAddedWasSpace = true;
                    }
                }
                else
                {
                    lastAddedWasSpace = false;
                }
            }

            return chrToAdd;
        }

        private void NormalizeCharsList(
            List<char> charsList,
            int maxLength)
        {
            int charsCount = charsList.Count;

            if (charsCount > maxLength)
            {
                charsList.RemoveRange(
                    maxLength,
                    charsCount - maxLength);
            }
        }
    }
}
