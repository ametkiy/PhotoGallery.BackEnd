using Microsoft.EntityFrameworkCore;
using PhotoGalary.Model;
using System.Threading.Tasks;

namespace PhotoGalary.Data
{
    public class PhotoGalaryContext : DbContext, IPhotoGalaryContext
    {
        public DbSet<Album> Albums { get; set; }
        public DbSet<Photo> Photos { get; set; }

        public PhotoGalaryContext(DbContextOptions<PhotoGalaryContext> options)
            : base(options)
        { }

        public async Task<int> SaveChanges()
        {
            return await base.SaveChangesAsync();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress; DataBase=PhotoGalary; trusted_connection=true;");
        }
    }
}
