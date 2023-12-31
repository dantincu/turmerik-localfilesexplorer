using Serilog.Events;
using Turmerik.LocalFilesExplorer.AspNetCoreApp.Utility;

namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.Logging
{
    public class TrmrkLogEventPropertyValue : LogEventPropertyValue
    {
        public TrmrkLogEventPropertyValue(
            IStringTemplateToken token,
            object propVal)
        {
            Token = token ?? throw new ArgumentNullException(nameof(token));
            PropVal = propVal;
        }

        public IStringTemplateToken Token { get; }
        public object PropVal { get; }

        public override void Render(
            TextWriter output,
            string format = null,
            IFormatProvider formatProvider = null)
        {
            string templateStr = Token.ToStrTemplate(0);

            string propValue = string.Format(
                templateStr,
                PropVal);

            output.Write(propValue);
        }
    }
}
