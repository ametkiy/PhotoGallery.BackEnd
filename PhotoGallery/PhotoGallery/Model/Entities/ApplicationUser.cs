using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace PhotoGallery.Model.Entities
{
    public class ApplicationUser : IdentityUser 
    {
        public string FirsName { get; set; }
        public string LastName { get; set; }

        public List<Photo> Photos { get; set; }
    }
}
