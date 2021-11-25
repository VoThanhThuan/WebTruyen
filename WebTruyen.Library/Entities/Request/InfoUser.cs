using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebTruyen.Library.Entities.Request
{
    public class InfoUser
    {
        public string Nickname { get; set; } = "";
        public DateTime Dob { get; set; } = DateTime.Now;
        public bool sex { get; set; } = true;
        public string Address { get; set; } = "";
        public string Fanpage { get; set; } = "";
        public string Email { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public string Username { get; set; } = "";

    }
}
