using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhotoGalary.Model
{
    public class Album : BaseModel
    {
        //[MaxLength(80)]
        public string Title { get; set; }
        public string Description { get; set; }

        public List<Photo> Photos { get; set; } = new List<Photo>();

    }
}
