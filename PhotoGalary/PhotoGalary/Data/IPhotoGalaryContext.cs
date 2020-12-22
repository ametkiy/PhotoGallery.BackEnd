using Microsoft.EntityFrameworkCore;
using PhotoGalary.Model;
using System.Threading.Tasks;

namespace PhotoGalary.Data
{
    public interface IPhotoGalaryContext
    {
        DbSet<Album> Albums { get; set; }
        DbSet<Photo> Photos { get; set; }

        Task<int> SaveChanges();
    }
}