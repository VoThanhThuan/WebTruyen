using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebTruyen.Library.Entities;

namespace WebTruyen.Library.Data.Configurations
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
