using Microsoft.AspNetCore.Http;

namespace PhotoGallery.Exceptions
{
    public class ConflictNewUserException : BaseException
    {
        public override int ErrorCode { get; } = StatusCodes.Status409Conflict;
        public ConflictNewUserException()
        {
        }

        public ConflictNewUserException(string email)
            : base($"An account has already been registered for this email '{email}'.")
        {
        }
    }
}
