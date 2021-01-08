using PhotoGalary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoGallery.Model.Entities
{
    public class Tag:BaseModel
    {
        public string Name { get; set; }

        public List<Album> Albums{ get; set; }
        public List<Photo> Photos { get; set; }
    }
}
