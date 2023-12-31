using Turmerik.LocalFilesExplorer.AspNetCoreApp.Utility;

namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.Logging
{
    public interface IAppLoggerCreatorFactory
    {
        IAppLoggerCreator Create(
            bool useAppProcessIdnfByDefault = false);
    }

    public class AppLoggerCreatorFactory : IAppLoggerCreatorFactory
    {
        private readonly IJsonConversion jsonConversion;
        private readonly IAppEnv appEnv;
        private readonly IAppLoggerConfig appLoggerConfig;
        private readonly IAppInstanceStartInfoProvider appInstanceStartInfoProvider;
        private readonly ITimeStampHelper timeStampHelper;
        private readonly ITrmrkJsonFormatterFactory trmrkJsonFormatterFactory;
        private readonly IStringTemplateParser stringTemplateParser;

        public AppLoggerCreatorFactory(
            IJsonConversion jsonConversion,
            IAppEnv appEnv,
            IAppLoggerConfig appLoggerConfig,
            IAppInstanceStartInfoProvider appInstanceStartInfoProvider,
            ITimeStampHelper timeStampHelper,
            ITrmrkJsonFormatterFactory trmrkJsonFormatterFactory,
            IStringTemplateParser stringTemplateParser)
        {
            this.jsonConversion = jsonConversion ?? throw new ArgumentNullException(nameof(jsonConversion));
            this.appEnv = appEnv ?? throw new ArgumentNullException(nameof(appEnv));
            this.appLoggerConfig = appLoggerConfig ?? throw new ArgumentNullException(nameof(appLoggerConfig));
            this.appInstanceStartInfoProvider = appInstanceStartInfoProvider ?? throw new ArgumentNullException(nameof(appInstanceStartInfoProvider));
            this.timeStampHelper = timeStampHelper ?? throw new ArgumentNullException(nameof(timeStampHelper));
            this.trmrkJsonFormatterFactory = trmrkJsonFormatterFactory ?? throw new ArgumentNullException(nameof(trmrkJsonFormatterFactory));
            this.stringTemplateParser = stringTemplateParser ?? throw new ArgumentNullException(nameof(stringTemplateParser));
        }

        public IAppLoggerCreator Create(
            bool useAppProcessIdnfByDefault = false) => new AppLoggerCreator(
                jsonConversion,
                appEnv,
                appLoggerConfig,
                appInstanceStartInfoProvider,
                timeStampHelper,
                trmrkJsonFormatterFactory,
                stringTemplateParser,
                useAppProcessIdnfByDefault);
    }
}
