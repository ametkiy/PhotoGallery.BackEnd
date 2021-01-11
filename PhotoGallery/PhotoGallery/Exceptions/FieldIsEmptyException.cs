using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PhotoGallery.Exceptions
{
    public class FieldIsEmptyException: BaseException
    {
        public override HttpStatusCode ErrorCode { get; } = HttpStatusCode.BadRequest;
        public FieldIsEmptyException()
        {
        }

        public FieldIsEmptyException(string message)
            : base(message)
        {
        }
    }
}
