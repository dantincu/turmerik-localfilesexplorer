namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.Utility
{
    public interface ITextBufferLinesRetriever
    {
        bool RetrieveLines(
            TextLinesRetrieverOpts opts,
            RetrievedTextLines retObj,
            int readCount);

        bool ShouldStopDigestingChars(
            TextLinesRetrieverOpts opts,
            RetrievedTextLines retObj,
            int lastBuffNwLnIdx);

        string GetTextLine(
            TextLinesRetrieverOpts opts,
            bool keepNwLnChars,
            int firstCharIdx,
            int lastCharIdx);

        string? RemoveControlCharsIfReq(
            TextLinesRetrieverOpts opts,
            string? str,
            bool keepNwLnChars,
            bool throwIfNwLnCharsFound = false);

        string GetTextLineWithoutControlChars(
            TextLinesRetrieverOpts opts,
            string str,
            bool keepNwLnChars);

        string GetTextLineWithoutControlChars(
            TextLinesRetrieverOpts opts,
            bool keepNwLnChars,
            int firstCharIdx,
            int lastCharIdx);

        bool ShouldKeepControlChar(
            bool keepNwLnChars,
            bool keepTabChars,
            char chr,
            int chIdx,
            int firstCharIdx,
            int lastCharIdx);
    }

    public class TextBufferLinesRetriever : ITextBufferLinesRetriever
    {
        public bool RetrieveLines(
            TextLinesRetrieverOpts opts,
            RetrievedTextLines retObj,
            int readCount)
        {
            int lastBuffNwLnIdx = -1;
            int linesCount = 0;
            var buff = opts.CharsBuffer;

            for (int i = 0; i < readCount; i++)
            {
                var chr = buff[i];

                if (chr == '\n')
                {
                    string line = GetTextLine(
                        opts, opts.KeepNwLnChars,
                        lastBuffNwLnIdx, i);

                    if (retObj.LastChunk != null)
                    {
                        line += RemoveControlCharsIfReq(
                            opts, retObj.LastChunk, true, true);

                        retObj.LastChunk = null;
                    }

                    retObj.LastLineMatches = opts.StopPredicate(
                        retObj, lastBuffNwLnIdx, line, i);

                    retObj.Lines.Add(line);
                    lastBuffNwLnIdx = i;
                    linesCount++;

                    if (retObj.LastLineMatches)
                    {
                        break;
                    }
                }
            }

            int lastChunkStIdx = lastBuffNwLnIdx + 1;
            int lastChunkLen = readCount - lastChunkStIdx;

            if (lastChunkLen > 0)
            {
                retObj.LastChunk = new string(
                    buff, lastChunkStIdx,
                    lastChunkLen);
            }
            else
            {
                retObj.LastChunk = null;
            }

            retObj.LastNwLnIdx += lastChunkStIdx;
            retObj.LastReadCharIdx += readCount;

            retObj.ReachedEndOfStream = readCount < buff.Length;

            bool shouldStopDigestingChars = ShouldStopDigestingChars(
                opts, retObj, lastBuffNwLnIdx);

            if (shouldStopDigestingChars && lastChunkLen > 0 && opts.RemoveControlChars)
            {
                retObj.LastChunk = GetTextLineWithoutControlChars(
                    opts, retObj.LastChunk!, true);
            }

            return shouldStopDigestingChars;
        }

        public bool ShouldStopDigestingChars(
            TextLinesRetrieverOpts opts,
            RetrievedTextLines retObj,
            int lastBuffNwLnIdx)
        {
            bool retVal = retObj.LastLineMatches || retObj.ReachedEndOfStream;
            retVal = retVal || lastBuffNwLnIdx == -1 || retObj.Lines.Count >= opts.MaxLinesCount;
            return retVal;
        }

        public string GetTextLine(
            TextLinesRetrieverOpts opts,
            bool keepNwLnChars,
            int firstCharIdx,
            int lastCharIdx)
        {
            string line;

            if (opts.RemoveControlChars)
            {
                line = GetTextLineWithoutControlChars(
                    opts, keepNwLnChars,
                    firstCharIdx,
                    lastCharIdx);
            }
            else
            {
                line = new string(
                    opts.CharsBuffer,
                    firstCharIdx + 1,
                    lastCharIdx - firstCharIdx);
            }

            return line;
        }

        public string? RemoveControlCharsIfReq(
            TextLinesRetrieverOpts opts,
            string? str,
            bool keepNwLnChars,
            bool throwIfNwLnCharsFound = false)
        {
            if (str != null && opts.RemoveControlChars)
            {
                str = GetTextLineWithoutControlChars(
                    opts, str,
                    keepNwLnChars,
                    0, str.Length - 1,
                    throwIfNwLnCharsFound);
            }

            return str;
        }

        public string GetTextLineWithoutControlChars(
            TextLinesRetrieverOpts opts,
            string str,
            bool keepNwLnChars) => GetTextLineWithoutControlChars(
                opts, str,
                keepNwLnChars,
                0, str.Length - 1);

        public string GetTextLineWithoutControlChars(
            TextLinesRetrieverOpts opts,
            bool keepNwLnChars,
            int firstCharIdx,
            int lastCharIdx) => GetTextLineWithoutControlChars(
                opts, opts.CharsBuffer,
                keepNwLnChars,
                firstCharIdx,
                lastCharIdx);

        public string GetTextLineWithoutControlChars(
            TextLinesRetrieverOpts opts,
            IEnumerable<char> charsNmrbl,
            bool keepNwLnChars,
            int firstCharIdx,
            int lastCharIdx,
            bool throwIfNwLnCharsFound = false)
        {
            bool replTabs = !string.IsNullOrEmpty(
                opts.TabReplacingStr);

            var charsArr = charsNmrbl.Where(
                (ch, i) => ShouldKeepControlChar(
                    keepNwLnChars, opts.KeepTabs || replTabs, ch, i,
                    firstCharIdx,
                    lastCharIdx)).ToArray();

            string line = new string(charsArr);

            if (throwIfNwLnCharsFound && line.Contains('\n'))
            {
                throw new InvalidOperationException(
                    $"The following string is not supposed to contain new line chars: ${line}");
            }

            if (replTabs)
            {
                line = line.Replace("\t", opts.TabReplacingStr);
            }

            return line;
        }

        public bool ShouldKeepControlChar(
            bool keepNwLnChars,
            bool keepTabChars,
            char chr,
            int chIdx,
            int firstCharIdx,
            int lastCharIdx)
        {
            bool retVal = chIdx >= firstCharIdx && chIdx <= lastCharIdx;

            if (retVal)
            {
                retVal = !char.IsControl(chr);

                if (!retVal)
                {
                    switch (chr)
                    {
                        case '\n':
                            retVal = keepNwLnChars;
                            break;
                        case '\t':
                            retVal = keepTabChars;
                            break;
                    }
                }
            }

            return retVal;
        }
    }
}
