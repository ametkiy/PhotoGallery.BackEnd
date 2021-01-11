using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PhotoGallery.Exceptions
{
    public class FileSizeException : BaseException
    {
        public override HttpStatusCode ErrorCode { get; } = HttpStatusCode.BadRequest;
        public FileSizeException()
        {
        }

        public FileSizeException(string message)
            : base(message)
        {
        }

        public FileSizeException(string fileName, long fileSize, long fileSizeLimit)
            : base($"The File '{fileName}' has size {fileSize} exceeds the maximum file size {fileSizeLimit}.")
        {
        }

    }
}
