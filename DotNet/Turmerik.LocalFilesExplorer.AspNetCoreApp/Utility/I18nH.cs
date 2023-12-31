using System.Globalization;

namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.Utility
{
    public static class I18nH
    {
        public static CultureInfo ToCultureInfo(string cultureCode)
        {
            CultureInfo cultureInfo;

            if (!string.IsNullOrWhiteSpace(cultureCode))
            {
                cultureInfo = new CultureInfo(cultureCode);
            }
            else
            {
                cultureInfo = CultureInfo.InvariantCulture;
            }

            return cultureInfo;
        }
    }
}
