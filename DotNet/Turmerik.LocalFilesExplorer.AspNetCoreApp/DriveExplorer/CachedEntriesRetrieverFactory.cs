using System;
using System.Collections.Generic;
using System.Text;

namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.DriveExplorer
{
    public interface ICachedEntriesRetrieverFactory
    {
        ICachedFsEntriesRetriever FsEntriesRetriever(
            DriveItem rootFolder,
            char dirSeparator);

        ICachedCsEntriesRetriever CsEntriesRetriever(
            DriveItem rootFolder);
    }

    public class CachedEntriesRetrieverFactory : ICachedEntriesRetrieverFactory
    {
        public ICachedFsEntriesRetriever FsEntriesRetriever(
            DriveItem rootFolder,
            char dirSeparator) => new CachedFsEntriesRetriever(
                rootFolder, dirSeparator);

        public ICachedCsEntriesRetriever CsEntriesRetriever(
            DriveItem rootFolder) => new CachedCsEntriesRetriever(
                rootFolder);
    }
}
