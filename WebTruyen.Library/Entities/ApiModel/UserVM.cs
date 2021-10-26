using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using WebTruyen.Library.Entities.Request;

namespace WebTruyen.Library.Entities.ApiModel
{
    public class UserAM
    {
        public User ToUser()
        {
            return new User()
            {
                Id = Id,
                Nickname = Nickname,
                Dob = Dob,
                Avatar = Avatar,
                sex = sex,
                Address = Address,
                Fanpage = Fanpage,
                Email = Email,
                PhoneNumber = PhoneNumber,
                UserName = Username,
            };
        }

        public UserRequest ToRequest()
        {
            return new UserRequest()
            {
                Id = Id,
                Nickname = Nickname,
                Dob = Dob,
                sex = sex,
                Address = Address,
                Fanpage = Fanpage,
                Email = Email,
                PhoneNumber = PhoneNumber,
                Username = Username
            };
        }
        public Guid Id { get; set; }
        public string Nickname { get; set; }
        [Display(Name = "Ngày sinh")]
        [DataType(DataType.Date)]
        public DateTime? Dob { get; set; }
        public string Avatar { get; set; } = "";
        public bool sex { get; set; } = true;
        public string Address { get; set; } = "";
        public string Fanpage { get; set; } = "";

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public string Username { get; set; }

        public Guid IdRole { get; set; }
        public string RoleName { get; set; }


    }
}
