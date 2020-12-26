using PhotoGalary.Model;
using PhotoGallery.Model.AbstractClasses;
using System.Collections.Generic;


namespace PhotoGallery.Model.DTO
{
    public class AlbumDto : BaseModelDto
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public List<Photo> Photos { get; set; } = new List<Photo>();
    }
}
