namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.Utility
{
    public static class ListH
    {
        public static void AddItems<T>(
            this List<T> list,
            params T[] itemsArr)
        {
            list.AddRange(itemsArr);
        }
    }
}
