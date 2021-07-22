using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebTruyen.API.Entities;

namespace WebTruyen.API.Data.Configurations
{
    public class ReportConfiguration : IEntityTypeConfiguration<Report>
    {
        public void Configure(EntityTypeBuilder<Report> builder)
        {
            builder.ToTable("Report");
            builder.HasKey(x => new { x.IdUser, x.IdChapter });
            builder.HasOne(x => x.User).WithMany(x => x.Reports)
                .HasForeignKey(x => x.IdUser);
            builder.HasOne(x => x.Chapter).WithMany(x => x.Reports)
                .HasForeignKey(x => x.IdChapter);

        }
    }
}
