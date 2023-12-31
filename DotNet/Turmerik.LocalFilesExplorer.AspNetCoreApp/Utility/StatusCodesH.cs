using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.Utility
{
    public static class StatusCodesH
    {
        /// <summary>
        /// According to https://developer.mozilla.org/en-US/docs/Web/HTTP/Status#client_error_responses : <br />
        /// The origin server requires the request to be conditional. This response is intended to prevent the 'lost update' problem, where a client GETs a resource's state,
        /// modifies it and PUTs it back to the server, when meanwhile a third party has modified the state on the server, leading to a conflict.
        /// </summary>
        public const int STATUS_428_PRECONDITION_REQUIRED = 428;
    }
}
