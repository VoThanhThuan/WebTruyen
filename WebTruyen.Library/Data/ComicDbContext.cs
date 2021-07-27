using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using WebTruyen.Library.Data.Configurations;
using WebTruyen.Library.Entities;

namespace WebTruyen.Library.Data
{
    public class ComicDbContext : DbContext
    {
        public ComicDbContext(DbContextOptions<ComicDbContext> options) : base(options)
        { }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (optionsBuilder.IsConfigured) return;
        //    var configuration = new ConfigurationBuilder()
        //        .SetBasePath(Directory.GetCurrentDirectory())
        //        .AddJsonFile("appsettings.json")
        //        .Build();
        //    optionsBuilder.UseSqlServer(configuration.GetConnectionString("ConnectComic"));
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ComicConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new GenreConfiguration());

            modelBuilder.ApplyConfiguration(new ComicInGenreConfiguration());

            modelBuilder.ApplyConfiguration(new ChapterConfiguration());
            modelBuilder.ApplyConfiguration(new PageConfiguration());

            modelBuilder.ApplyConfiguration(new BookmarkConfiguration());

            modelBuilder.ApplyConfiguration(new HistoryReadConfiguration());

            modelBuilder.ApplyConfiguration(new NewComicAnnouncementConfiguration());

            modelBuilder.ApplyConfiguration(new ReportConfiguration());

            modelBuilder.ApplyConfiguration(new TranslationOfUserConfiguration());

            modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("AppUserRole").HasKey(x => new { x.UserId, x.RoleId });
        }

        public DbSet<Comic> Comics { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<ComicInGenre> ComicInGenres { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<Chapter> Chapters { get; set; }
        public DbSet<TranslationOfUser> TranslationOfUsers { get; set; }
        public DbSet<Bookmark> Bookmarks { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Announcement> NewComicAnnouncements { get; set; }
        public DbSet<HistoryRead> HistoryReads { get; set; }
        public DbSet<Report> Report { get; set; }
    }
}
