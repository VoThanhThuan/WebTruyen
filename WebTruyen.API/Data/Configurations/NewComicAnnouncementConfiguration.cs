using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebTruyen.API.Entities;

namespace WebTruyen.API.Data.Configurations
{
    public class NewComicAnnouncementConfiguration : IEntityTypeConfiguration<NewComicAnnouncement>
    {
        public void Configure(EntityTypeBuilder<NewComicAnnouncement> builder)
        {
            builder.ToTable("NewComicAnnouncement");
            builder.HasKey(x => new { x.IdUser, x.IdChapter });
            builder.HasOne(x => x.User).WithMany(x => x.NewComicAnnouncements)
                .HasForeignKey(x => x.IdUser);
            builder.HasOne(x => x.Chapter).WithMany(x => x.NewComicAnnouncements)
                .HasForeignKey(x => x.IdChapter);
        }
    }
}
