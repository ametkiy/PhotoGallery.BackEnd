using Microsoft.AspNetCore.Http;
using System;

namespace PhotoGallery.Exceptions
{
    public class PhotoNotFoundException : BaseException
    {
        public override int ErrorCode { get; } = StatusCodes.Status404NotFound;
        public PhotoNotFoundException()
        {
        }

        public PhotoNotFoundException(string message)
            : base(message)
        {
        }

        public PhotoNotFoundException(Guid id)
            : base($"Photo with id '{id}' not found.")
        {
        }
    }
}
