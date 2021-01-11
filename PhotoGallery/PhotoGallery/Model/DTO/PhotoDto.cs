using PhotoGallery.Model.AbstractClasses;
using System;

namespace PhotoGallery.Model.DTO
{
    public class PhotoDto : BaseModelDto
    {
        public string FileName { get; set; }
        public string Description { get; set; }

        public Guid? AlbumId { get; set; }

        public byte[] PhotoData { get; set; }

        public DateTime AddDate { get; set; }
        public string Tags { get; set; }
    }
}
