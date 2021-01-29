using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PhotoGallery.Model;
using PhotoGallery.Model.Entities;
using System;

namespace PhotoGallery.Data
{
    public class PhotoGalleryContext : IdentityDbContext<ApplicationUser>, IPhotoGalleryContext
    {
        public DbSet<Album> Albums { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public PhotoGalleryContext(DbContextOptions<PhotoGalleryContext> options)
                : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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
                .Property(p => p.FileMimeType).IsRequired().HasMaxLength(255);
            modelBuilder.Entity<Photo>()
                .HasOne(p => p.Album)
                .WithMany(t => t.Photos)
                .HasForeignKey(p => p.AlbumId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Photo>()
                .HasMany(p => p.Likes)
                .WithOne(l => l.Photo)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.Likes)
                .WithOne(l => l.User)
                .OnDelete(DeleteBehavior.Cascade); 

            modelBuilder.Entity<Photo>()
                .HasMany(t => t.Tags)
                .WithMany(a => a.Photos)
                .UsingEntity(u => u.ToTable("PhotosTags"));

            modelBuilder.Entity<Photo>()
                .HasOne(p => p.ApplicationUser)
                .WithMany(u => u.Photos)
                .HasForeignKey(p => p.ApplicationUserId)
                .OnDelete(DeleteBehavior.ClientCascade); ;

            modelBuilder.Entity<Tag>()
                .ToTable("Tags").HasKey(p => p.Id);
            modelBuilder.Entity<Tag>().Property(t => t.Name).IsRequired().HasMaxLength(100);

            modelBuilder.Entity<Like>()
                .ToTable("Likes").HasKey(p => p.Id);

            modelBuilder.Entity<ApplicationUser>()
                .Property(p => p.FirstName).HasMaxLength(80);

            modelBuilder.Entity<ApplicationUser>()
                .Property(p => p.LastName).HasMaxLength(80);
        }
    }
}
