using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace WebTruyen.API.Entities
{
    public class User : IdentityUser<Guid>
    {
        public string Nickname { get; set; }
        public DateTime Dob { get; set; }
        public string Avatar { get; set; }
        public bool? sex { get; set; }
        public string Address { get; set; }
        public string Fanpage { get; set; }
    }
}
