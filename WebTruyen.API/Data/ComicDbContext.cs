using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebTruyen.API.Data.Configurations;
using WebTruyen.API.Entities;

namespace WebTruyen.API.Data
{
    public class ComicDbContext : DbContext
    {
        public ComicDbContext(DbContextOptions<ComicDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ComicInGenreConfiguration());

        }

        public DbSet<Comic> Comics { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<ComicInGenre> ComicInGenres { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<Chapter> Chapters { get; set; }
        public DbSet<TranslationOfUser> TranslationOfUsers { get; set; }
        public DbSet<Bookmark> Bookmarks { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<NewComicAnnouncement> NewComicAnnouncements { get; set; }
        public DbSet<HistoryRead> HistoryReads { get; set; }
    }
}
