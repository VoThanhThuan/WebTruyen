using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace WebTruyen.Library.Entities.Request
{
    public class RegisterRequest
    {
        public UserRequest ToUserRequest()
        {
            return new UserRequest() {
                Nickname = Nickname,
                Dob = Dob,
                Avatar = Avatar,
                sex = sex ?? true,
                Address = Address,
                Fanpage = Fanpage,
                Email = Email,
                PhoneNumber = PhoneNumber,
                Username = Username,
                Password = Password,
                ConfirmPassword = ConfirmPassword
            };
        }

        [Required(ErrorMessage = "Nickname là bắt buộc")]
        public string Nickname { get; set; }

        //[Required(ErrorMessage = "Ngày sinh là bắt buộc")]
        public DateTime? Dob { get; set; }
        public IFormFile Avatar { get; set; }
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
    }
}
