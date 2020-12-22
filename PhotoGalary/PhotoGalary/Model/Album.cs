using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhotoGalary.Model
{
    public class Album : Base
    {
        [MaxLength(80)]
        public string Title { get; set; }
        public string Description { get; set; }

        public List<Photo> Photos { get; set; }



        public Album()
        {
            Photos = new List<Photo>();
        }
    }
}
