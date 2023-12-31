using Serilog.Events;
using Serilog.Parsing;
using Turmerik.LocalFilesExplorer.AspNetCoreApp.Utility;

namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.Logging
{
    public class TrmrkMessageTemplateToken : MessageTemplateToken
    {
        public TrmrkMessageTemplateToken(IStringTemplateToken token)
        {
            Token = token ?? throw new ArgumentNullException(nameof(token));
        }

        public IStringTemplateToken Token { get; }
        public override int Length => Token.Length;

        public override void Render(
            IReadOnlyDictionary<string, LogEventPropertyValue> properties,
            TextWriter output,
            IFormatProvider formatProvider = null)
        {
            if (Token is StringTemplateToken templateToken)
            {
                var propVal = properties[templateToken.Idx.ToString()];

                propVal.Render(
                    output,
                    templateToken.Format,
                    formatProvider);
            }
            else if (Token is StringLiteralToken literalToken)
            {
                output.Write(literalToken.Value);
            }
            else
            {
                throw new NotSupportedException();
            }
        }
    }
}
