using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PhotoGallery.Exceptions
{
    public abstract class BaseException : Exception
    {
        public abstract HttpStatusCode ErrorCode { get; }

        public BaseException() { }
        public BaseException(string message)
            : base(message)
        {
        }
    }
}
