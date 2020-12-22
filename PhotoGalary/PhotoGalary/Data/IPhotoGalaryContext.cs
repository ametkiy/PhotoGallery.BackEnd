using Microsoft.EntityFrameworkCore;
using PhotoGalary.Model;
using System.Threading;
using System.Threading.Tasks;

namespace PhotoGalary.Data
{
    public interface IPhotoGalaryContext
    {
        DbSet<Album> Albums { get; set; }
        DbSet<Photo> Photos { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}