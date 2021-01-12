using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoGallery.Model.DTO
{
    public class PhotoDataDto : PhotoDto
    {
        public byte[] PhotoData { get; set; }
    }
}
