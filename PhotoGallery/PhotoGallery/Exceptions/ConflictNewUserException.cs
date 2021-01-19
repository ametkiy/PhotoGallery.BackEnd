using Microsoft.AspNetCore.Http;

namespace PhotoGallery.Exceptions
{
    public class ConflictNewUserException : BaseException
    {
        public override int ErrorCode { get; } = StatusCodes.Status409Conflict;
        public ConflictNewUserException()
        {
        }

        public ConflictNewUserException(string message)
            : base(message)
        {
        }
    }
}
