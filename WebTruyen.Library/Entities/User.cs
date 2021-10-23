using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WebTruyen.Library.Entities.ViewModel;

namespace WebTruyen.Library.Entities
{
    public class User : IdentityUser<Guid>
    {
        public UserVM ToViewModel()
        {
            return new UserVM()
            {
                Id = Id,
                Nickname = Nickname,
                Avatar = Avatar,
                Dob = Dob,
                sex = sex,
                Address = Address,
                Fanpage = Fanpage,
                Email = Email,
                PhoneNumber = PhoneNumber,
                Username = UserName,
                Password = PasswordHash
        };
        }

        public async Task<UserVM> ToViewModel(UserManager<Library.Entities.User> userManager)
        {
            var user = new UserVM()
            {
                Id = Id,
                Nickname = Nickname,
                Avatar = Avatar,
                Dob = Dob,
                sex = sex,
                Address = Address,
                Fanpage = Fanpage,
                Email = Email,
                PhoneNumber = PhoneNumber,
                Username = UserName,
                Password = PasswordHash
            };
            user.RoleName = (await userManager.GetRolesAsync(this))[0];
            return user;
        }

        public string Nickname { get; set; } = "";
        public DateTime? Dob { get; set; }
        public string Avatar { get; set; } = "";
        public bool sex { get; set; } = true;
        public string Address { get; set; } = "";
        public string Fanpage { get; set; } = "";
        //Khóa ngoại
        public virtual List<Report> Reports { get; set; }
        public virtual List<TranslationOfUser> TranslationOfUsers { get; set; }
        public virtual List<Bookmark> Bookmarks { get; set; }
        public virtual List<Announcement> NewComicAnnouncements { get; set; }
        public virtual List<HistoryRead> HistoryReads { get; set; }
        public virtual List<Comment> Comments { get; set; }


    }
}
