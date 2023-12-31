namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.Utility
{
    public interface IControlCharsNormalizer
    {
        List<char> NormalizeControlChars(
            IEnumerable<char> inputNmrbl,
            Dictionary<char, string> controlCharsMap);

        public List<char> NormalizeControlChars(
            IEnumerable<char> inputNmrbl,
            string tabStr = null);
    }

    public class ControlCharsNormalizer : IControlCharsNormalizer
    {
        public List<char> NormalizeControlChars(
            IEnumerable<char> inputNmrbl,
            Dictionary<char, string> controlCharsMap)
        {
            var retList = new List<char>();

            foreach (char c in inputNmrbl)
            {
                if (char.IsControl(c))
                {
                    if (controlCharsMap.TryGetValue(
                        c, out string str))
                    {
                        if (str == null)
                        {
                            retList.Add(c);
                        }
                        else if (str.Length > 0)
                        {
                            retList.AddRange(str);
                        }
                    }
                }
            }

            return retList;
        }

        public List<char> NormalizeControlChars(
            IEnumerable<char> inputNmrbl,
            string tabStr = null) => NormalizeControlChars(
                inputNmrbl, new Dictionary<char, string>
                {
                    { '\n', null },
                    { ' ', null },
                    { '\t', tabStr }
                });
    }
}
