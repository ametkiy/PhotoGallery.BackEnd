using PhotoGallery.Model;
using PhotoGallery.Model.AbstractClasses;
using PhotoGallery.Model.Entities;
using System.Collections.Generic;


namespace PhotoGallery.Model.DTO
{
    public class AlbumDto : BaseModelDto
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public List<PhotoDto> Photos { get; set; } = new List<PhotoDto>();
        public List<TagDto> Tags { get; set; } = new List<TagDto>();
    }
}
