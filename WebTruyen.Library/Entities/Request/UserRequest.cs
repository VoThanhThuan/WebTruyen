﻿using System;
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
            return new User()
            {
                Id = Id,
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

        public Guid Id { get; set; }

        [Required(ErrorMessage = "Nickname là bắt buộc")]
        public string Nickname { get; set; } = "";
        [Required(ErrorMessage = "Ngày sinh là bắt buộc")]
        [DataType(DataType.Date)]
        public DateTime? Dob { get; set; }
        public IFormFile? Avatar { get; set; }
        [Required]
        public bool? sex { get; set; } = true;
        public string Address { get; set; } = "";
        public string Fanpage { get; set; } = "";

        [DataType(DataType.EmailAddress)] 
        public string Email { get; set; } = "";

        public string PhoneNumber { get; set; } = "";
        [Required(ErrorMessage = "Tên đăng nhập là bắt buộc")]
        public string Username { get; set; } = "";

        [Required(ErrorMessage = "Mật khẩu là bắt buộc")]
        [DataType(DataType.Password)] 
        public string Password { get; set; } = "";

        [Required(ErrorMessage = "Xác nhận mật khẩu là bắt buộc")]
        [DataType(DataType.Password)] 
        public string ConfirmPassword { get; set; } = "";
        public Guid IdRole { get; set; }

    }
}
