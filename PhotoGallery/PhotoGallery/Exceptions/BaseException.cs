using System;

namespace PhotoGallery.Exceptions
{
    public abstract class BaseException : Exception
    {
        public abstract int ErrorCode { get; }

        public BaseException() { }
        public BaseException(string message)
            : base(message)
        {
        }
    }
}
