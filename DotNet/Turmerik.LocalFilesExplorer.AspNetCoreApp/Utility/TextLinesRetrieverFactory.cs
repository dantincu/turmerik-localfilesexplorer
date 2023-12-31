namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.Utility
{
    public interface ITextLinesRetrieverFactory
    {
        ITextLinesRetriever CreateFromTextReader(
            TextReader textReader);
    }

    public interface ITextLinesRetriever
    {
        RetrievedTextLines RetrieveLinesUntil(
            TextLinesRetrieverOpts opts);

        Task<RetrievedTextLines> RetrieveLinesUntilAsync(
            TextLinesRetrieverOpts opts);
    }

    public class TextLinesRetrieverFactory : ITextLinesRetrieverFactory
    {
        private readonly ITextBufferLinesRetriever textBufferLinesRetriever;

        public TextLinesRetrieverFactory(
            ITextBufferLinesRetriever textBufferLinesRetriever)
        {
            this.textBufferLinesRetriever = textBufferLinesRetriever ?? throw new ArgumentNullException(nameof(textBufferLinesRetriever));
        }

        public ITextLinesRetriever CreateFromTextReader(
            TextReader textReader) => new TextReaderLinesRetriever(
                textBufferLinesRetriever, textReader);
    }

    public class TextLinesRetrieverOpts
    {
        public char[] CharsBuffer { get; set; }
        public Func<RetrievedTextLines, int, string, int, bool> StopPredicate { get; set; }
        public int MaxLinesCount { get; set; }
        public int MaxLineLength { get; set; }
        public bool RemoveControlChars { get; set; }
        public bool KeepNwLnChars { get; set; }
        public bool KeepTabs { get; set; }
        public string TabReplacingStr { get; set; }
    }

    public class RetrievedTextLines
    {
        public List<string> Lines { get; set; }
        public string? LastChunk { get; set; }
        public bool LastLineMatches { get; set; }
        public bool ReachedEndOfStream { get; set; }
        public int LastNwLnIdx { get; set; }
        public int LastReadCharIdx { get; set; }
    }
}
