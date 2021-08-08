using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;
using WebTruyen.Library.Enums;

namespace WebTruyen.UI.Admin.RequestClient
{
    public class ComicRequestClient
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = "";
        public string NameAlias { get; set; } = "";
        public string AnotherNameOfComic { get; set; } = "";
        public string Author { get; set; } = "";
        public Status? Status { get; set; } = 0;
        public int? Views { get; set; } = 0;
        public string Description { get; set; } = "";
        public IBrowserFile Thumbnail { get; set; } = null;
    }
}
