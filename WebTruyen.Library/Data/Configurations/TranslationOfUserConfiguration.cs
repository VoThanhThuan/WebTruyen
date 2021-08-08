using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebTruyen.Library.Entities;

namespace WebTruyen.Library.Data.Configurations
{
    public class TranslationOfUserConfiguration : IEntityTypeConfiguration<TranslationOfUser>
    {
        public void Configure(EntityTypeBuilder<TranslationOfUser> builder)
        {
            builder.ToTable("TranslationOfUser");
            builder.HasKey(x => new { x.IdUser, x.IdComic });
            builder.HasOne(x => x.User).WithMany(x => x.TranslationOfUsers)
                .HasForeignKey(x => x.IdUser);
            builder.HasOne(x => x.Comic).WithMany(x => x.TranslationOfUsers)
                .HasForeignKey(x => x.IdComic);

        }
    }
}
