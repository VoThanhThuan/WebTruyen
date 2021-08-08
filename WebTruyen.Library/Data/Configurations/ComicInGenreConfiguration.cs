using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebTruyen.Library.Entities;

namespace WebTruyen.Library.Data.Configurations
{
    public class ComicInGenreConfiguration : IEntityTypeConfiguration<ComicInGenre>
    {
        public void Configure(EntityTypeBuilder<ComicInGenre> builder)
        {
            builder.ToTable("ComicInGenre");
            builder.HasKey(x => new { x.IdGenre, x.IdComic });
            builder
                .HasOne(x => x.Genre)
                .WithOne(x => x.ComicInGenre)
                .HasForeignKey<ComicInGenre>(x => x.IdGenre);
            builder
                .HasOne(x => x.Comic)
                .WithMany(x => x.ComicInGenres)
                .HasForeignKey(x => x.IdComic);
        }
    }
}
