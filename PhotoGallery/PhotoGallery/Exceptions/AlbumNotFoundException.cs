using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PhotoGallery.Exceptions
{
    public class AlbumNotFoundException : BaseException
    {
        public override HttpStatusCode ErrorCode { get; } = HttpStatusCode.NotFound;

        public AlbumNotFoundException()
        {
        }

        public AlbumNotFoundException(string message)
            : base(message)
        {
        }

        public AlbumNotFoundException(Guid id)
            : base($"Album with id {id} not found.")
        {
        }
    }
}
