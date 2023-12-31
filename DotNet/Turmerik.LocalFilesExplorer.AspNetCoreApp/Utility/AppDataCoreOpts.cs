using System;
using System.Collections.Generic;
using System.Text;

namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.Utility
{
    public class AppDataCoreOpts<TImmtbl, TMtblSrlzbl>
        where TImmtbl : class
    {
        public Func<TMtblSrlzbl> DefaultDataFactory { get; set; }
        public Func<TMtblSrlzbl, TImmtbl> DataNormalizer { get; set; }
        public Func<TImmtbl, TMtblSrlzbl> DataSerializer { get; set; }
    }
}
