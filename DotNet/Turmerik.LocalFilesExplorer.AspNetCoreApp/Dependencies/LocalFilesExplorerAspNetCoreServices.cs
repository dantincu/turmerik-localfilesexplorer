using Turmerik.LocalFilesExplorer.AspNetCoreApp.DriveExplorer;
using Turmerik.LocalFilesExplorer.AspNetCoreApp.Utility;

namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.Dependencies
{
    public static class LocalFilesExplorerAspNetCoreServices
    {
        public static IServiceCollection RegisterAll(
            IServiceCollection services)
        {
            services.AddSingleton<ITimeStampHelper, TimeStampHelper>();
            services.AddSingleton<IFsItemsRetriever, FsItemsRetriever>();
            services.AddSingleton<IDriveExplorerService, FsExplorerService>();

            return services;
        }
    }
}
