using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebTruyen.API.Entities;

namespace WebTruyen.API.Data.Configurations
{
    public class HistoryReadConfiguration : IEntityTypeConfiguration<HistoryRead>
    {
        public void Configure(EntityTypeBuilder<HistoryRead> builder)
        {
            builder.ToTable("HistoryRead");
            builder.HasKey(x => new {x.IdUser, x.IdComic});
            builder.HasOne(x => x.User).WithMany(x => x.HistoryReads)
                .HasForeignKey(x => x.IdUser);
            builder.HasOne(x => x.Comic).WithMany(x => x.HistoryReads)
                .HasForeignKey(x => x.IdComic);

        }
    }
}
