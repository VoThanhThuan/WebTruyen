using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebTruyen.Library.Entities;

namespace WebTruyen.Library.Data
{
    public class ComicSeed
    {
        private readonly IPasswordHasher<User> _passwordHasher = new PasswordHasher<User>();

        public async Task SeedAsync(ComicDbContext context, ILogger<ComicSeed> logger)
        {
            var ra = Guid.NewGuid();
            var re = Guid.NewGuid();
            var rg = Guid.NewGuid();
            if (!context.Roles.Any())
            {
                var admin = new Role()
                {
                    Id = ra,
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                };
                context.Roles.Add(admin);

                var editer = new Role()
                {
                    Id = re,
                    Name = "Editer",
                    NormalizedName = "EDITER"
                };
                context.Roles.Add(editer);

                var guest = new Role()
                {
                    Id = rg,
                    Name = "Guest",
                    NormalizedName = "GUEST"
                };
                context.Roles.Add(guest);
            }


            if (!context.Users.Any())
            {
                var idu = Guid.NewGuid();
                var user = new User()
                {
                    Id = idu,
                    Nickname = "Thuan",
                    Email = "admin1@gmail.com",
                    NormalizedEmail = "ADMIN1@GMAIL.COM",
                    PhoneNumber = "0123456789",
                    UserName = "admin",
                    NormalizedUserName = "ADMIN",
                    SecurityStamp = Guid.NewGuid().ToString()
                };
                user.PasswordHash = _passwordHasher.HashPassword(user, "Admin123$");
                context.Users.Add(user);

                var modelBuilder = new ModelBuilder();
                modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
                {
                    RoleId = ra,
                    UserId = idu
                });
            }


            if (!context.Genres.Any())
            {
                var manga = new Genre()
                {
                    Name = "Manga",
                    Description = "Truyện tranh của Nhật"
                };
                context.Genres.Add(manga);

                var manhwa = new Genre()
                {
                    Name = "Manhwa",
                    Description = "Truyện tranh của Hàn"
                };
                context.Genres.Add(manhwa);

                var manhua = new Genre()
                {
                    Name = "Manhua",
                    Description = "Truyện tranh của Trung"
                };
                context.Genres.Add(manhua);

            }


            await context.SaveChangesAsync();
        }
    }
}
