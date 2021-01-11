using Microsoft.EntityFrameworkCore;
using PhotoGallery.Model;
using PhotoGallery.Model.Entities;

namespace PhotoGallery.Data
{
    public class PhotoGalleryContext : DbContext, IPhotoGalleryContext
    {
        public DbSet<Album> Albums { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public PhotoGalleryContext(DbContextOptions<PhotoGalleryContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Album>()
                .ToTable("Albums").HasKey(p => p.Id);
            modelBuilder.Entity<Album>()
                .Property(p => p.Title).IsRequired().HasMaxLength(80);
            modelBuilder.Entity<Album>()
                .HasMany<Tag>(t => t.Tags)
                .WithMany(a => a.Albums)
                .UsingEntity(u => u.ToTable("AlbumsTags"));

            modelBuilder.Entity<Photo>()
                .ToTable("Photos").HasKey(p => p.Id);
            modelBuilder.Entity<Photo>()
                .Property(p => p.FileName).IsRequired().HasMaxLength(260);
            modelBuilder.Entity<Photo>()
                .HasOne<Album>(p => p.Album)
                .WithMany(t => t.Photos)
                .HasForeignKey(p => p.AlbumId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Photo>()
                .HasMany<Tag>(t => t.Tags)
                .WithMany(a => a.Photos)
                .UsingEntity(u => u.ToTable("PhotosTags"));

            modelBuilder.Entity<Tag>()
                .ToTable("Tags").HasKey(p => p.Id);
            modelBuilder.Entity<Tag>().Property(t => t.Name).IsRequired().HasMaxLength(100);


        }
    }
}
