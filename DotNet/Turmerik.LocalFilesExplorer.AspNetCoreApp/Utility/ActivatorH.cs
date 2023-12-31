namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.Utility
{
    public static class ActivatorH
    {
        public static T CreateFromSrc<T>(
            this object src,
            Type type = null,
            params object[] argsArr)
        {
            type ??= typeof(T);
            argsArr = src.Arr(argsArr);

            var retObj = (T)Activator.CreateInstance(
                type, argsArr);

            return retObj;
        }
    }
}
