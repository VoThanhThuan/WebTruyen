using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using WebTruyen.Library.Entities.ViewModel;

namespace WebTruyen.UI.Admin.RequestClient
{
    public class UserRequestClient
    {
        public void ConverRequest(UserVM user)
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
        public string Nickname { get; set; } = "";
        public DateTime? Dob { get; set; }
        public (string data, string filename) Avatar { get; set; }
        public bool sex { get; set; } = true;
        public string Address { get; set; } = "";
        public string Fanpage { get; set; } = "";
        public string Email { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
        public string ConfirmPassword { get; set; } = "";
        public Guid IdRole { get; set; } = Guid.Empty;
    }
}
