using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;

namespace WebTruyen.UI.Admin.RequestClient
{
    public class UserRequestClient
    {
        public Guid Id { get; set; }
        public string Nickname { get; set; }
        public DateTime? Dob { get; set; }
        public IBrowserFile Avatar { get; set; }
        public bool sex { get; set; } = true;
        public string Address { get; set; } = "";
        public string Fanpage { get; set; } = "";
        public string Email { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public Guid? IdRole { get; set; }
    }
}
