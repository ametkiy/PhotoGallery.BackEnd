using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoGallery.Exceptions
{
    public class AlbumNotFoundException :Exception
    {
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
