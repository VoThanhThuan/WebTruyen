using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebTruyen.UI.Client.Model
{
    public class RegisterRequestClient
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Nickname là bắt buộc")]
        public string Nickname { get; set; }

        [Required(ErrorMessage = "Ngày sinh là bắt buộc")]
        [DataType(DataType.Date)]
        public DateTime Dob { get; set; } = DateTime.Today;
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
    }
}
