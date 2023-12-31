using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.Utility
{
    public interface IStringTemplateParser
    {
        Dictionary<int, IStringTemplateToken> Parse(string templateStr);
    }

    public class StringTemplateParser : IStringTemplateParser
    {
        public Dictionary<int, IStringTemplateToken> Parse(
            string templateStr) => ForEachTemplateStrChar(
                new Args(templateStr), args =>
                {
                    if (args.IsInsideToken)
                    {
                        if (args.Char == '}')
                        {
                            args.IsInsideToken = false;
                            AddStringTemplateToken(args);
                            args.TokenStartIdx = args.Idx + 1;
                        }
                    }
                    else
                    {
                        if (args.PrevChar == '{')
                        {
                            if (args.Char != '{')
                            {
                                args.IsInsideToken = true;

                                if (args.Idx > 1 && args.TemplateStr[args.Idx - 2] != '}')
                                {
                                    AddStringLiteralToken(args);
                                }

                                args.TokenStartIdx = args.Idx - 1;
                            }
                        }
                    }
                },
                args =>
                {
                    if (args.IsInsideToken)
                    {
                        throw new InvalidOperationException();
                    }

                    if (args.Idx > args.TokenStartIdx)
                    {
                        args.Idx++;
                        AddStringLiteralToken(args);
                    }

                }).Tokens;

        private Args ForEachTemplateStrChar(
            Args args,
            Action<Args> callback,
            Action<Args> completeCallback)
        {
            while (args.Idx < args.TemplateStrLen)
            {
                args.PrevChar = args.Char;
                args.Char = args.TemplateStr[args.Idx];
                callback(args);

                args.Idx++;
            }

            completeCallback(args);
            return args;
        }

        private StringTemplateToken GetStringTemplateToken(Args args)
        {
            string tokenStr = args.TemplateStr.Substring(
                args.TokenStartIdx,
                args.Idx - args.TokenStartIdx + 1);

            string[] tokenParts = tokenStr.Split(':');

            var tokenObj = new StringTemplateToken(
                int.Parse(tokenParts.First().Trim('{', '}')),
                tokenParts.Skip(1).FirstOrDefault()?.TrimEnd('}'));

            return tokenObj;
        }

        private StringLiteralToken GetStringLiteralToken(Args args)
        {
            string tokenStr = args.TemplateStr.Substring(
                args.TokenStartIdx,
                args.Idx - args.TokenStartIdx - 1);

            var tokenObj = new StringLiteralToken(tokenStr);
            return tokenObj;
        }

        private void AddStringTemplateToken(Args args) => args.Tokens.Add(
            args.TokenStartIdx,
            GetStringTemplateToken(args));

        private void AddStringLiteralToken(Args args) => args.Tokens.Add(
            args.TokenStartIdx,
            GetStringLiteralToken(args));

        private class Args
        {
            public Args(string templateStr)
            {
                TemplateStr = templateStr ?? throw new ArgumentNullException(nameof(templateStr));
                TemplateStrLen = templateStr.Length;
                Tokens = new Dictionary<int, IStringTemplateToken>();
            }

            public string TemplateStr { get; }
            public int TemplateStrLen { get; }
            public Dictionary<int, IStringTemplateToken> Tokens { get; }

            public int Idx { get; set; }
            public char PrevChar { get; set; }
            public char Char { get; set; }
            public bool IsInsideToken { get; set; }
            public int TokenStartIdx { get; set; }
        }
    }

    public interface IStringTemplateToken
    {
        int Length { get; }

        string ToStrTemplate(int? idx = null);
    }

    public abstract class StringTemplateTokenBase : IStringTemplateToken
    {
        public abstract int Length { get; }

        public abstract string ToStrTemplate(int? idx = null);
    }

    public class StringLiteralToken : StringTemplateTokenBase
    {
        public StringLiteralToken(string value)
        {
            Value = value ?? throw new ArgumentNullException(nameof(value));
            Length = value.Length;
        }

        public string Value { get; }
        public override int Length { get; }

        public override string ToStrTemplate(int? idx = null) => Value;
    }

    public class StringTemplateToken : StringTemplateTokenBase
    {
        public StringTemplateToken(
            int idx,
            string format)
        {
            Idx = idx;
            Format = format;
        }

        public override int Length { get; }
        public int Idx { get; }
        public string Format { get; }

        public override string ToStrTemplate(int? idx = null)
        {
            string str = null;
            string format = Format;

            if (!string.IsNullOrEmpty(format))
            {
                str = $":{format}";
            }

            str = $"{{{idx ?? Idx}{str}}}";
            return str;
        }

        private int GetLength()
        {
            int len = 2 * Idx.ToString().Length;

            if (!string.IsNullOrEmpty(Format))
            {
                len += Format.Length + 1;
            }

            return len;
        }
    }
}
