using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace WebTruyen.API.Service
{
    public interface IWaterMarkService
    {
        public Task<string> SaveImage(IFormFile file, string path, bool security = false);
    }
}
