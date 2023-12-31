using System;
using System.Collections.Generic;
using System.Text;

namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.Utility
{
    public readonly struct JsonSerializerOpts
    {
        public JsonSerializerOpts(
            bool useCamelCase,
            bool ignoreNullValues,
            bool useStringEnumConverter)
        {
            UseCamelCase = useCamelCase;
            IgnoreNullValues = ignoreNullValues;
            UseStringEnumConverter = useStringEnumConverter;
        }

        public bool UseCamelCase { get; }
        public bool IgnoreNullValues { get; }
        public bool UseStringEnumConverter { get; }
    }
}
