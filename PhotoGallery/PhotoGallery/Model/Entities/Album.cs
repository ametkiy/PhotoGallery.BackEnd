using System.Collections.Generic;

namespace PhotoGalary.Model
{
    public class Album : BaseModel
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public List<Photo> Photos { get; set; } = new List<Photo>();

    }
}
