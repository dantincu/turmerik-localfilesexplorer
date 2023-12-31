using System.Collections.ObjectModel;
using System.Security.Cryptography;
using System.Text;

namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.Utility
{
    public static class EncodeH
    {
        public static byte[] EncodeSha1(byte[] input)
        {
            byte[] hash;

            using (var sha1 = SHA1.Create())
            {
                hash = sha1.ComputeHash(input);
            }

            return hash;
        }

        public static byte[] EncodeSha1(string input) => EncodeSha1(
            Encoding.UTF8.GetBytes(input));

        public static byte[] EncodeSha256(byte[] input)
        {
            byte[] hash;

            using (SHA256 mySHA256 = SHA256.Create())
            {
                hash = mySHA256.ComputeHash(input);
            }

            return hash;
        }

        public static byte[] EncodeSha256(string input) => EncodeSha256(
            Encoding.UTF8.GetBytes(input));

        public static byte[] TryDecodeFromBase64(string str)
        {
            byte[] retVal = null;

            try
            {
                if (str != null)
                {
                    retVal = Convert.FromBase64String(str);
                }
            }
            catch
            {
            }

            return retVal;
        }

        public static string EncodeToBase64String(
            byte[] bytesArr,
            char[] lastTwoChars = null,
            char paddingChar = Base64Chars.PADDING)
        {
            string base64String = Convert.ToBase64String(bytesArr);

            if (paddingChar != '=' || lastTwoChars != null)
            {
                lastTwoChars = lastTwoChars ?? Base64Chars.LastChars.ToArray();

                var replDictnr = new Dictionary<char, char>
                {
                    { Base64Chars.SECOND_LAST_BIT , lastTwoChars[1] },
                    { Base64Chars.LAST_BIT, lastTwoChars[0] },
                    { Base64Chars.PADDING, paddingChar }
                };

                base64String = base64String.ReplaceAllChars(replDictnr);
            }

            return base64String;
        }

        public static byte[] EncodeSha512(byte[] input)
        {
            byte[] hash;

            using (var sha3 = SHA512.Create())
            {
                hash = sha3.ComputeHash(input);
            }

            return hash;
        }

        public static byte[] EncodeSha512(string input) => EncodeSha512(
            Encoding.UTF8.GetBytes(input));

        public static string GetUserIdnfHash(
            string userIdnfTpl,
            string username)
        {
            string userIdnf = string.Format(
                userIdnfTpl, username);

            var userIdnfHashBytes = EncodeSha512(userIdnf);
            var userIdnfHash = EncodeToBase64String(userIdnfHashBytes);

            return userIdnfHash;
        }

        public static class Base64Chars
        {
            public const char PADDING = '=';
            public const char LAST_BIT = '/';
            public const char SECOND_LAST_BIT = '+';

            public static readonly ReadOnlyCollection<char> LastChars;
            public static readonly ReadOnlyCollection<char> AllSpecialChars;

            static Base64Chars()
            {
                LastChars = new char[] { SECOND_LAST_BIT, LAST_BIT }.RdnlC();
                AllSpecialChars = new char[] { SECOND_LAST_BIT, LAST_BIT, PADDING }.RdnlC();
            }
        }
    }
}
