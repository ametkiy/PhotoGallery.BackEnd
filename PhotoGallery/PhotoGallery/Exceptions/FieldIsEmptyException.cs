using Microsoft.AspNetCore.Http;

namespace PhotoGallery.Exceptions
{
    public class FieldIsEmptyException: BaseException
    {
        public override int ErrorCode { get; } = StatusCodes.Status400BadRequest;
        public FieldIsEmptyException()
        {
        }

        public FieldIsEmptyException(string message)
            : base(message)
        {
        }
    }
}
