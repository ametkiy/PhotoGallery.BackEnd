using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PhotoGallery.Model;
using PhotoGallery.Model.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace PhotoGallery.Data
{
    public interface IPhotoGalleryContext
    {
        DbSet<Album> Albums { get; set; }
        DbSet<Photo> Photos { get; set; }
        DbSet<Tag> Tags { get; set; }
        DbSet<Like> Likes { get; set; }
        DbSet<ApplicationUser> ApplicationUsers { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    }
}