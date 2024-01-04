using System.Runtime.Serialization;

namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.DriveExplorer
{
    public class DriveExplorerException : Exception
    {
        public DriveExplorerException()
        {
        }

        public DriveExplorerException(string? message) : base(message)
        {
        }

        public DriveExplorerException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected DriveExplorerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
