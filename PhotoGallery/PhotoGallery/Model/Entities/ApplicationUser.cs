using Microsoft.AspNetCore.Identity;

namespace PhotoGallery.Model.Entities
{
    public class ApplicationUser : IdentityUser 
    {
        public string FirsName { get; set; }
        public string LastName { get; set; }
    }
}
