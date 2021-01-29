using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoGallery.Model.Entities
{
    public class Like : BaseModel
    {
        public Photo Photo { get; set; }
        public ApplicationUser User { get; set; }
        public DateTime LikedTime { get; set; } = DateTime.Now;
    }
}
