using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoGallery.Exceptions
{
    public class PhotoNotFoundException : Exception
    {
        public PhotoNotFoundException()
        {
        }

        public PhotoNotFoundException(string message)
            : base(message)
        {
        }

        public PhotoNotFoundException(Guid id)
            : base($"Photo with id {id} not found.")
        {
        }
    }
}
