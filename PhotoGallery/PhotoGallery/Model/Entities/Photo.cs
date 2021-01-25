using PhotoGallery.Model.Entities;
using System;
using System.Collections.Generic;

namespace PhotoGallery.Model
{
    public class Photo : BaseModel
    {
        public string FileName { get; set; }
        public string Description { get; set; }

        public Guid? AlbumId { get; set; }
        public Album Album { get; set; }

        public byte[] PhotoData { get; set; }

        public DateTime AddDate { get; set; }

        public List<Tag> Tags { get; set; } = new List<Tag>();

        public string FileMimeType { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
        public String ApplicationUserId { get; set; }
        public bool Private { get; set; } = false;
    }
}
