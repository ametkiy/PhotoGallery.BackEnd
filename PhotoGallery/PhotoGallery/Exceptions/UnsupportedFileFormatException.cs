using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoGallery.Exceptions
{
    public class UnsupportedFileFormatException : Exception
    {
        public UnsupportedFileFormatException()
        {
        }

        public UnsupportedFileFormatException(string fileName)
            : base($"File '{fileName}' contains an unsupported data format.")
        {
        }
    }
}
