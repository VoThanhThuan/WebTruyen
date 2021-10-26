using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebTruyen.Library.Entities;

namespace WebTruyen.Library.Data.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Comment");
            builder.HasKey(x => new { x.Id });

            builder.Property(x => x.Content).HasMaxLength(500);

            builder.HasOne(x => x.Chapter).WithMany(x => x.Comments).HasForeignKey(x => x.IdChapter);
            builder.HasOne(x => x.Comic).WithMany(x => x.Comments).HasForeignKey(x => x.IdComic);
            builder.HasOne(x => x.User).WithMany(x => x.Comments).HasForeignKey(x => x.IdUser);
        }
    }
}
