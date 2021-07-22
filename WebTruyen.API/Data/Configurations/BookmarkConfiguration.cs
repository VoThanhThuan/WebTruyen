using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebTruyen.API.Entities;

namespace WebTruyen.API.Data.Configurations
{
    public class BookmarkConfiguration : IEntityTypeConfiguration<Bookmark>
    {
        public void Configure(EntityTypeBuilder<Bookmark> builder)
        {
            builder.ToTable("Bookmark");
            builder.HasKey(x => new {x.IdUser, x.IdComic});
            builder.HasOne(x => x.Comic).WithMany(x => x.Bookmarks)
                .HasForeignKey(x => x.IdComic);
            builder.HasOne(x => x.User).WithMany(x => x.Bookmarks)
                .HasForeignKey(x => x.IdUser);
        }
    }
}
