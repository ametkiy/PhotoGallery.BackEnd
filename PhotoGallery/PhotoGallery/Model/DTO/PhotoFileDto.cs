using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoGallery.Model.DTO
{
    public class PhotoFileDto
    {
        public byte[] PhotoData { get; set; }
        public string FileMimeType { get; set; }
    }
}
