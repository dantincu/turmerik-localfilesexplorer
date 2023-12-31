using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.Utility
{
    public static class UtilsH
    {
        public static T SafeCast<T>(
            this object obj,
            Func<T> defaultValueFactory = null)
        {
            T retVal;

            if (obj is T value)
            {
                retVal = value;
            }
            else if (defaultValueFactory != null)
            {
                retVal = defaultValueFactory();
            }
            else
            {
                retVal = default;
            }

            return retVal;
        }

        public static bool IsDefault<T>(
            this T value,
            IEqualityComparer<T> eqCompr = null)
        {
            eqCompr ??= EqualityComparer<T>.Default;
            bool isDefault = eqCompr.Equals(value, default);

            return isDefault;
        }
    }
}
