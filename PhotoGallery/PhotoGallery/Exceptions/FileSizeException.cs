using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoGallery.Exceptions
{
    public class FileSizeException : Exception
    {
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
