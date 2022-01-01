using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace WebTruyen.UI.Admin.Service
{
    public class HttpContextBlazor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpContextBlazor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public HttpContext Context => _httpContextAccessor.HttpContext;
    }
}
