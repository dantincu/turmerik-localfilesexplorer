namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.Utility
{
    public static class MutexH
    {
        public static Mutex Create(
            string filePath,
            out bool createdNew,
            bool isGlobal = false,
            bool initiallyOwned = false)
        {
            string mutexName = GetMutexName(
                filePath, isGlobal);

            var mutex = new Mutex(
                initiallyOwned,
                mutexName,
                out createdNew);

            return mutex;
        }

        public static Mutex Create(
            string filePath,
            bool isGlobal = false,
            bool initiallyOwned = false) => Create(
                filePath,
                out _,
                isGlobal,
                initiallyOwned);

        public static string GetMutexName(
            string filePath,
            bool isGlobal = false)
        {
            filePath = filePath.Replace('\\', '/');

            if (isGlobal)
            {
                filePath = $"Global\\{filePath}";
            }
            else
            {
                filePath = $"Local\\{filePath}";
            }

            return filePath;
        }
    }
}
