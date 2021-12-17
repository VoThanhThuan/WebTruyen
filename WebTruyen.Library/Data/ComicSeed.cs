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
                    Name = "Reader",
                    NormalizedName = "READER"
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
                    Avatar = "/avatar/45348869-1020-4646-86c3-65a8dde2952f.jpg",
                    SecurityStamp = Guid.NewGuid().ToString()
                };
                user.PasswordHash = _passwordHasher.HashPassword(user, "admin");
                context.Users.Add(user);
                await context.SaveChangesAsync();

                context.Add(new IdentityUserRole<Guid>
                {
                    RoleId = ra,
                    UserId = idu,
                });
                await context.SaveChangesAsync();

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

                var vietnam = new Genre() {
                    Name = "Việt Nam",
                    Description = "Truyện tranh của Việt Nam"
                };
                context.Genres.Add(vietnam);

                var adult = new Genre()
                {
                    Name = "Adult",
                    Description = "These webtoon (Manga, Manhwa, Manhua) depict an explicit level of sexual content, indicated by one or more of the following: detailed, graphic sequences; a high frequency of sexual content, even if content isn't explicit; prevalent nudity; Fetish-based, graphic sequences; or simulated sexual acts."
                };
                context.Genres.Add(adult);

                var action = new Genre() {
                    Name = "adult",
                    Description = "Action webtoon (Manga, Manhwa, Manhua) is about conflict. Whether with guns, blades, fists, or mysterious powers, these manga feature characters in combat - either to protect themselves or the things or people they value, or simply as a way of life."
                };
                context.Genres.Add(action);

                var adventure = new Genre() {
                    Name = "Adventure",
                    Description = "Thể loại phiêu lưu"
                };
                context.Genres.Add(adventure);

                var comedy = new Genre() {
                    Name = "Comedy",
                    Description = "These webtoon (Manga, Manhwa, Manhua) aim to make you laugh through satire, parody, humorous observations, slapstick scenarios, or absurd antics. Bonus points for spitting your drink all over your screen!"
                };
                context.Genres.Add(comedy);

                var cooking = new Genre() {
                    Name = "Cooking",
                    Description = "Cooking is the focus of these food-themed webtoon, whether the characters within attend a Culinary School, work in a Restaurant or are simply passionate home cooks. These manga may offer step-by-step Recipes for various dishes or plating techniques."
                };
                context.Genres.Add(cooking);

                var detective = new Genre() {
                    Name = "Detective",
                    Description = "Detectives are people who investigate crimes. These manga feature characters who work the streets, analyze evidence from crime scenes, or research past records, using logic and reasoning to discover who did what, and how. Detectives may be members of a police force, employed by some other organization, or private citizens with a desire to learn the truth."
                };
                context.Genres.Add(manhua);

                var fantasy = new Genre() {
                    Name = "Fantasy",
                    Description = "Fantasy webtoon (manga, manhwa or manhua) take place in a broad range of settings influenced by mythologies, legends, or popular and defining works of the genre such as The Lord of the Rings. They are generally characterized by a low level of technological development, though fantasy stories can just as easily take place in our modern world, or in a Post-apocalyptic society where technology was buried alongside the old world. These manga also tend to feature magic or other extraordinary abilities, strange or mysterious creatures, or humanoid races which coexist with humanity or inhabit their own lands removed from ours."
                };
                context.Genres.Add(fantasy);

                var schoolLife = new Genre() {
                    Name = "School Life",
                    Description = "These manga showcase events that occur on a daily basis in a school, whether from the perspective of a student or of a teacher. Having fun in the classroom, attending School Clubs, spending time with friends or doing daily chores are frequent themes in these manga."
                };
                context.Genres.Add(schoolLife); 

                var shounen = new Genre() {
                    Name = "Shounen",
                    Description = "Shounen webtoon is a genres for Younger Male Audience, also romanized as shonen or shounen, are Japanese comics marketed towards young teen males between the ages of 12 and 18."
                };
                context.Genres.Add(shounen);
            }


            await context.SaveChangesAsync();
        }
    }
}
