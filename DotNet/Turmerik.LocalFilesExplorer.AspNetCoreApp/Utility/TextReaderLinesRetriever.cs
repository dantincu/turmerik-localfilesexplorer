using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.Utility
{
    public class TextReaderLinesRetriever : ITextLinesRetriever, IDisposable
    {
        private readonly ITextBufferLinesRetriever textBufferLinesRetriever;
        private readonly TextReader textReader;

        public TextReaderLinesRetriever(
            ITextBufferLinesRetriever textBufferLinesRetriever,
            TextReader textReader)
        {
            this.textBufferLinesRetriever = textBufferLinesRetriever ?? throw new ArgumentNullException(
                nameof(textBufferLinesRetriever));

            this.textReader = textReader ?? throw new ArgumentNullException(
                nameof(textReader));
        }

        public void Dispose()
        {
            textReader.Dispose();
        }

        public RetrievedTextLines RetrieveLinesUntil(
            TextLinesRetrieverOpts opts)
        {
            var retObj = GetRetObj();
            var buff = opts.CharsBuffer ??= new char[opts.MaxLineLength];
            bool shouldStopDigestingChars = false;

            while (!shouldStopDigestingChars)
            {
                int readCount = textReader.ReadBlock(
                    buff, 0, buff.Length);

                shouldStopDigestingChars = textBufferLinesRetriever.RetrieveLines(
                    opts, retObj, readCount);
            }

            return retObj;
        }

        public async Task<RetrievedTextLines> RetrieveLinesUntilAsync(
            TextLinesRetrieverOpts opts)
        {
            var retObj = GetRetObj();
            var buff = opts.CharsBuffer ??= new char[opts.MaxLineLength];
            bool shouldStopDigestingChars = false;

            while (!shouldStopDigestingChars)
            {
                int readCount = await textReader.ReadBlockAsync(
                    buff, 0, buff.Length);

                shouldStopDigestingChars = textBufferLinesRetriever.RetrieveLines(
                    opts, retObj, readCount);
            }

            return retObj;
        }

        private RetrievedTextLines GetRetObj() => new RetrievedTextLines
        {
            Lines = new List<string>(),
            LastNwLnIdx = -1,
            LastReadCharIdx = -1,
        };
    }
}
