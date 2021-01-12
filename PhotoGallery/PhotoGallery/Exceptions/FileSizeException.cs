using Microsoft.AspNetCore.Http;

namespace PhotoGallery.Exceptions
{
    public class FileSizeException : BaseException
    {
        public override int ErrorCode { get; } = StatusCodes.Status400BadRequest;
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
