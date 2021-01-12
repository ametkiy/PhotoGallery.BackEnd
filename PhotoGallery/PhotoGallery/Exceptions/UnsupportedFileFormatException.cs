using Microsoft.AspNetCore.Http;

namespace PhotoGallery.Exceptions
{
    public class UnsupportedFileFormatException : BaseException
    {
        public override int ErrorCode { get; } = StatusCodes.Status415UnsupportedMediaType;
        public UnsupportedFileFormatException()
        {
        }

        public UnsupportedFileFormatException(string fileName)
            : base($"File '{fileName}' contains an unsupported data format.")
        {
        }
    }
}
