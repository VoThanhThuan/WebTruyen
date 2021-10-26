using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using WebTruyen.Library.Entities.ApiModel;

namespace WebTruyen.UI.Admin.RequestClient
{
    public class UserRequestClient
    {
        public void ConverRequest(UserAM user)
        {
            this.Id = user.Id;
            this.Nickname = user.Nickname;
            this.Dob = user.Dob;
            this.sex = user.sex;
            this.Address = user.Address;
            this.Fanpage = user.Fanpage;
            this.Email = user.Email;
            this.PhoneNumber = user.PhoneNumber;
            this.Username = user.Username;
            this.IdRole = user.IdRole;
        }

        public Guid Id { get; set; }

        [Required(ErrorMessage = "Nickname là bắt buộc")]
        public string Nickname { get; set; }

        [Required(ErrorMessage = "Ngày sinh là bắt buộc")]
        [DataType(DataType.Date)]
        public DateTime? Dob { get; set; }
        public (string data, string filename) Avatar { get; set; }
        [Required]
        public bool? sex { get; set; } = true;
        public string Address { get; set; } = "";
        public string Fanpage { get; set; } = "";

        [DataType(DataType.Date)]
        public string Email { get; set; } = "";
        public string PhoneNumber { get; set; } = "";

        [Required(ErrorMessage = "Tên đăng nhập là bắt buộc")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Mật khẩu là bắt buộc")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Xác nhận mật khẩu là bắt buộc")]
        public string ConfirmPassword { get; set; }

        public Guid IdRole { get; set; } = Guid.Empty;
    }
}
