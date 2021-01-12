﻿using PhotoGallery.Model.AbstractClasses;
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
        public List<Tag> Tags { get; set; }
    }
}
