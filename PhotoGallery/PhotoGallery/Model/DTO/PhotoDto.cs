using PhotoGallery.Model.AbstractClasses;
using PhotoGallery.Model.Entities;
using System;
using System.Collections.Generic;

namespace PhotoGallery.Model.DTO
{
    public class PhotoDto : BaseModelDto
    {
        public string FileName { get; set; }
        public string Description { get; set; }

        public Guid? AlbumId { get; set; }

        public DateTime AddDate { get; set; }
        public List<TagDto> Tags { get; set; }

        public bool Private { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserId { get; set; }
        public int LikesCount { get; set; } = -1;
    }
}
