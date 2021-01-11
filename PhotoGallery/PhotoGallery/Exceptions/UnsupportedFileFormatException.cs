using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PhotoGallery.Exceptions
{
    public class UnsupportedFileFormatException : BaseException
    {
        public override HttpStatusCode ErrorCode { get; } = HttpStatusCode.UnsupportedMediaType;
        public UnsupportedFileFormatException()
        {
        }

        public UnsupportedFileFormatException(string fileName)
            : base($"File '{fileName}' contains an unsupported data format.")
        {
        }
    }
}
