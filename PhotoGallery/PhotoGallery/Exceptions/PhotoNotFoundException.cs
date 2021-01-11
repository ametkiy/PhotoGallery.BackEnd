using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PhotoGallery.Exceptions
{
    public class PhotoNotFoundException : BaseException
    {
        public override HttpStatusCode ErrorCode { get; } = HttpStatusCode.NotFound;
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
