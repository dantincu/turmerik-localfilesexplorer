using Turmerik.LocalFilesExplorer.AspNetCoreApp.Utility;

namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.Logging
{
    public interface ITrmrkJsonFormatterFactory
    {
        TrmrkJsonFormatter CreateFormatter();
    }

    public class TrmrkJsonFormatterFactory : ITrmrkJsonFormatterFactory
    {
        private readonly IJsonConversion jsonConversion;
        private readonly ITimeStampHelper timeStampHelper;
        private readonly IExceptionSerializer exceptionSerializer;

        public TrmrkJsonFormatterFactory(
            IJsonConversion jsonConversion,
            ITimeStampHelper timeStampHelper,
            IExceptionSerializer exceptionSerializer)
        {
            this.jsonConversion = jsonConversion ?? throw new ArgumentNullException(nameof(jsonConversion));
            this.timeStampHelper = timeStampHelper ?? throw new ArgumentNullException(nameof(timeStampHelper));
            this.exceptionSerializer = exceptionSerializer ?? throw new ArgumentNullException(nameof(exceptionSerializer));
        }

        public TrmrkJsonFormatter CreateFormatter(
            ) => new TrmrkJsonFormatter(
            jsonConversion,
            timeStampHelper,
            exceptionSerializer);
    }
}
