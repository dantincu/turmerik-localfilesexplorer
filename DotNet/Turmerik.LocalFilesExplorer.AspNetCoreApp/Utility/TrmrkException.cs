using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.Utility
{
    public class TrmrkException : Exception
    {
        public TrmrkException()
        {
        }

        public TrmrkException(string message) : base(message)
        {
        }

        public TrmrkException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TrmrkException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public virtual object GetAdditionalData() => null;
    }

    public class TrmrkException<TData> : TrmrkException
    {
        public TrmrkException(
            TData data = default)
        {
            AdditionalData = data;
        }

        public TrmrkException(
            string message,
            TData data = default) : base(
                message)
        {
            AdditionalData = data;
        }

        public TrmrkException(
            string message,
            Exception innerException,
            TData data = default) : base(
                message,
                innerException)
        {
            AdditionalData = data;
        }

        protected TrmrkException(
            SerializationInfo info,
            StreamingContext context,
            TData data = default) : base(
                info,
                context)
        {
            AdditionalData = data;
        }

        public TData AdditionalData { get; }

        public override object GetAdditionalData() => AdditionalData;
    }
}
