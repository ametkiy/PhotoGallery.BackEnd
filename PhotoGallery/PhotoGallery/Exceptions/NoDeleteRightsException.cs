using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoGallery.Exceptions
{
    public class NoDeleteRightsException : BaseException
    {
        public override int ErrorCode { get; } = StatusCodes.Status403Forbidden;
        public NoDeleteRightsException()
        {
        }

        public NoDeleteRightsException(string message)
            : base(message)
        {
        }
    
    }
}
