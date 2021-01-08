using Microsoft.EntityFrameworkCore;
using PhotoGalary.Model;
using PhotoGallery.Model.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace PhotoGalary.Data
{
    public interface IPhotoGalleryContext
    {
        DbSet<Album> Albums { get; set; }
        DbSet<Photo> Photos { get; set; }
        DbSet<Tag> Tags { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}