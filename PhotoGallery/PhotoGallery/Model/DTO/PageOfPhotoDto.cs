using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoGallery.Model.DTO
{
    public class PageOfPhotoDto
    {
        public PhotoDto[] Photos { get; set; }
        public int Count { get; set; }
    }
}
