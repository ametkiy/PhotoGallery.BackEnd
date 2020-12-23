using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoGallery.Exceptions
{
    public class FieldIsEmptyException: Exception
    {
        public FieldIsEmptyException()
        {
        }

        public FieldIsEmptyException(string message)
            : base(message)
        {
        }
    }
}
