using Turmerik.LocalFilesExplorer.AspNetCoreApp.DriveExplorer;
using Turmerik.LocalFilesExplorer.AspNetCoreApp.Logging;
using Turmerik.LocalFilesExplorer.AspNetCoreApp.Settings;
using Turmerik.LocalFilesExplorer.AspNetCoreApp.Utility;

namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.Dependencies
{
    public static class LocalFilesExplorerAspNetCoreServices
    {
        public static IServiceCollection RegisterAll(
            IServiceCollection services)
        {
            services.AddSingleton<IAppInstanceStartInfoProvider, AppInstanceStartInfoProvider>();
            services.AddSingleton<ITimeStampHelper, TimeStampHelper>();
            services.AddSingleton<ILambdaExprHelper, LambdaExprHelper>();
            services.AddSingleton<ILambdaExprHelperFactory, LambdaExprHelperFactory>();
            services.AddSingleton<IBasicComparerFactory, BasicComparerFactory>();
            services.AddSingleton<IBasicEqualityComparerFactory, BasicEqualityComparerFactory>();
            services.AddSingleton<IJsonConversion, JsonConversion>();

            services.AddSingleton<ISynchronizedAdapterFactory, SynchronizedAdapterFactory>();
            services.AddSingleton<IStringTemplateParser, StringTemplateParser>();
            services.AddSingleton<IExceptionSerializer, ExceptionSerializer>();

            services.AddSingleton<IControlCharsNormalizer, ControlCharsNormalizer>();
            services.AddSingleton<IDelimCharsExtractor, DelimCharsExtractor>();
            services.AddSingleton<ITextBufferLinesRetriever, TextBufferLinesRetriever>();
            services.AddSingleton<ITextLinesRetrieverFactory, TextLinesRetrieverFactory>();

            services.AddSingleton<IFsEntryNameNormalizer, FsEntryNameNormalizer>();
            services.AddSingleton<IDriveItemsCreator, DriveItemsCreator>();

            services.AddSingleton<IFsItemsRetriever, FsItemsRetriever>();
            services.AddSingleton<ICachedEntriesRetrieverFactory, CachedEntriesRetrieverFactory>();

            services.AddSingleton<IAppEnv, AppEnv>();
            services.AddSingleton<IAppLoggerConfig, AppLoggerConfig>();
            services.AddSingleton<ITrmrkJsonFormatterFactory, TrmrkJsonFormatterFactory>();
            services.AddSingleton<IAppLoggerCreatorFactory, AppLoggerCreatorFactory>();

            services.AddSingleton(
                svcProv => svcProv.GetRequiredService<IAppLoggerCreatorFactory>().Create());

            services.AddSingleton<FsExplorerServiceFactory>();

            services.AddSingleton<IDriveItemsRetriever>(
                svcProv => svcProv.GetRequiredService<FsExplorerServiceFactory>().Retriever());

            services.AddSingleton<IDriveExplorerService>(
                svcProv => svcProv.GetRequiredService<FsExplorerServiceFactory>().Explorer());

            services.AddSingleton<IAppConfigServiceFactory, AppConfigServiceFactory>();

            services.AddSingleton(
                svcProv => svcProv.GetRequiredService<IAppConfigServiceFactory>(
                    ).Service<NotesAppConfigImmtbl, NotesAppConfigMtbl>(null));
            
            services.AddSingleton<IUsersManager, UsersManager>();
            services.AddSingleton<IUsersIdnfStorage, LocalJsonFileUsersIdnfStorage>();
            return services;
        }

        public class FsExplorerServiceFactory
        {
            private readonly IAppConfigService<NotesAppConfigImmtbl> appSettingsRetriever;
            private readonly ITimeStampHelper timeStampHelper;

            public FsExplorerServiceFactory(
                IAppConfigService<NotesAppConfigImmtbl> appSettingsRetriever,
                ITimeStampHelper timeStampHelper)
            {
                this.appSettingsRetriever = appSettingsRetriever ?? throw new ArgumentNullException(
                    nameof(appSettingsRetriever));

                this.timeStampHelper = timeStampHelper ?? throw new ArgumentNullException(
                    nameof(timeStampHelper));
            }

            public FsExplorerService Explorer(
                ) => new FsExplorerService(
                    timeStampHelper)
                {
                    RootDirPath = appSettingsRetriever.Data.FsExplorerServiceReqRootPath
                };

            public FsItemsRetriever Retriever(
                ) => new FsItemsRetriever(
                    timeStampHelper)
                {
                    RootDirPath = appSettingsRetriever.Data.FsExplorerServiceReqRootPath
                };
        }
    }
}
