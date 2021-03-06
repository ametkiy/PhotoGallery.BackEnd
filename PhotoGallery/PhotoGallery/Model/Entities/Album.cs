﻿using PhotoGallery.Model.Entities;
using System.Collections.Generic;

namespace PhotoGallery.Model
{
    public class Album : BaseModel
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public List<Photo> Photos { get; set; } = new List<Photo>();

        public List<Tag> Tags {get;set;} = new List<Tag>();
    }
}
