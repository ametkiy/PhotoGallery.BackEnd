using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoGallery.Exceptions
{
    public class CreateNewUserException : BaseException
    {
        public override int ErrorCode { get; } = StatusCodes.Status400BadRequest;

        public CreateNewUserException()
        {
        }

        public CreateNewUserException(string message)
            : base(message)
        {
        }

        public CreateNewUserException(IEnumerable<IdentityError> errors) 
            : base(string.Join(Environment.NewLine, errors.Select(e => e.Description).ToArray()))
        {
        }
    }
}
