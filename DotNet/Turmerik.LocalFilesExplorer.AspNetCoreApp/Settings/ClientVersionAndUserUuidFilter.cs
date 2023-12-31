using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turmerik.LocalFileNotes.AspNetCoreApp.Controllers;
using Turmerik.LocalFilesExplorer.AspNetCoreApp.DriveExplorer;
using Turmerik.LocalFilesExplorer.AspNetCoreApp.Utility;

namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.Settings
{
    public class ClientVersionAndUserUuidFilter : IActionFilter
    {
        private readonly IAppConfigService<NotesAppConfigImmtbl> appSettingsRetriever;
        private readonly IUsersManager usersManager;

        public ClientVersionAndUserUuidFilter(
            IAppConfigService<NotesAppConfigImmtbl> appSettingsRetriever,
            IUsersManager usersManager)
        {
            this.appSettingsRetriever = appSettingsRetriever ?? throw new ArgumentNullException(
                nameof(appSettingsRetriever));

            this.usersManager = usersManager ?? throw new ArgumentNullException(
                nameof(usersManager));
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            bool requireValidClientUserUuid = context.Controller?.GetType() != typeof(AppConfigController);
            var exposedHeaders = new HashSet<string>();

            var clientVersionStr = context.HttpContext.Request.Headers[TrmrkHeaderNamesH.CLIENT_VERSION_HEADER_NAME].ToString();
            var clientUserUuidStr = context.HttpContext.Request.Headers[TrmrkHeaderNamesH.CLIENT_USER_UUID_HEADER_NAME].ToString();
            var authUserIdnf = context.HttpContext.GetLocalFilesUserIdnfAsync(usersManager).Result;
            // var authUserIdnf = Guid.Parse("ba488154c96e43a5b2cf97be662b5ddb");

            if (requireValidClientUserUuid && (!Guid.TryParse(clientUserUuidStr, out var clientUserUuid) || clientUserUuid != authUserIdnf.UserUuid))
            {
                AddExposedHeader(context, exposedHeaders, null, null,
                    TrmrkClientErrCode.InvalidUserIdnfHash.ToString());
            }

            if (!int.TryParse(clientVersionStr, out var clientVersionNum) || clientVersionNum < appSettingsRetriever.Data.RequiredClientVersion)
            {
                AddExposedHeader(context, exposedHeaders,
                    TrmrkHeaderNamesH.REQUIRED_CLIENT_VERSION_HEADER_NAME,
                    appSettingsRetriever.Data.RequiredClientVersion.ToString(),
                    TrmrkClientErrCode.InvalidClientVersion.ToString());
            }

            context.HttpContext.Response.Headers[HeaderNamesH.ACCESS_CONTROL_EXPOSE_HEADERS] = string.Join(", ", exposedHeaders);
        }

        private void AddExposedHeader(
            FilterContext context,
            HashSet<string> exposedHeaders,
            string newExposedHeader,
            string newHeaderValue,
            StringValues? clientErrCode,
            int? statusCode = null)
        {
            context.HttpContext.Response.StatusCode = statusCode ?? StatusCodesH.STATUS_428_PRECONDITION_REQUIRED;

            if (clientErrCode.HasValue)
            {
                context.HttpContext.Response.Headers[TrmrkHeaderNamesH.CLIENT_ERR_CODE_HEADER_NAME] = clientErrCode.Value;
                exposedHeaders.Add(TrmrkHeaderNamesH.CLIENT_ERR_CODE_HEADER_NAME);
            }
            
            if (!string.IsNullOrWhiteSpace(newExposedHeader))
            {
                context.HttpContext.Response.Headers[newExposedHeader] = newHeaderValue;
                exposedHeaders.Add(newExposedHeader);
            }
        }
    }
}
