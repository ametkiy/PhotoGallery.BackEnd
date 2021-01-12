using Microsoft.AspNetCore.Http;
using System;

namespace PhotoGallery.Exceptions
{
    public class AlbumNotFoundException : BaseException
    {
        public override int ErrorCode { get; } = StatusCodes.Status404NotFound;

        public AlbumNotFoundException()
        {
        }

        public AlbumNotFoundException(string message)
            : base(message)
        {
        }

        public AlbumNotFoundException(Guid id)
            : base($"Album with id '{id}' not found.")
        {
        }
    }
}
