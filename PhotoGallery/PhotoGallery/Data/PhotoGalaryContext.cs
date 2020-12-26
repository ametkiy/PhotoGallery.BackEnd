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

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //        optionsBuilder.UseSqlServer("Server=.\\SQLExpress; DataBase=PhotoGalary; trusted_connection=true;");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Album>()
                .ToTable("Albums").HasKey(p => p.Id);
            modelBuilder.Entity<Album>()
                .Property(p => p.Title).IsRequired().HasMaxLength(80);


            modelBuilder.Entity<Photo>()
                .ToTable("Photos").HasKey(p => p.Id);
            modelBuilder.Entity<Photo>()
                .Property(p => p.FileName).IsRequired().HasMaxLength(260);
            modelBuilder.Entity<Photo>()
                .HasOne(p => p.Album)
                .WithMany(t => t.Photos)
                .HasForeignKey(p => p.AlbumId);
        }
    }
}
