using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace WebTruyen.Library.Entities.Request
{
    public class UserRequest
    {
        public User ToUser()
        {
            return new User() {
                Id = (Guid)Id,
                Nickname = Nickname,
                Dob = Dob,
                sex = sex ?? true,
                Address = Address,
                Fanpage = Fanpage,
                Email = Email,
                PhoneNumber = PhoneNumber,
                UserName = Username,
                PasswordHash = Password
            };
        }

        public Guid? Id { get; set; } = Guid.Empty;

        //[Required(ErrorMessage = "Nickname là bắt buộc")]
        public string Nickname { get; set; } = "";
        public DateTime? Dob { get; set; } = DateTime.Now;
        public IFormFile Avatar { get; set; }
        public bool? sex { get; set; } = true;
        public string Address { get; set; } = "";
        public string Fanpage { get; set; } = "";

        public string Email { get; set; } = "";

        public string PhoneNumber { get; set; } = "";
        //[Required(ErrorMessage = "Tên đăng nhập là bắt buộc")]
        public string Username { get; set; } = "";

        //[Required(ErrorMessage = "Mật khẩu là bắt buộc")]
        public string Password { get; set; } = "";

        //[Required(ErrorMessage = "Xác nhận mật khẩu là bắt buộc")]
        public string ConfirmPassword { get; set; } = "";
        public Guid IdRole { get; set; } = Guid.Empty;

    }
}
