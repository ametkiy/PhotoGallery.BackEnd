using System;

namespace PhotoGalary.Model
{
    public class Photo : BaseModel
    {
        public string FileName { get; set; }
        public string Description { get; set; }

        public Guid? AlbumId { get; set; }
        public Album Album { get; set; }

        public byte[] PhotoData { get; set; }

        public DateTime AddDate { get; set; }
    }
}
