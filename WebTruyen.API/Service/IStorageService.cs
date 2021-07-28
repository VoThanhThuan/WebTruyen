using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace WebTruyen.API.Service
{
    public interface IStorageService
    {
        public string GetFileUrl(string fileName);

        public Task SaveFileAsync(Stream mediaBinaryStream, string fileName);

        public Task<int> DeleteFileAsync(string fileName);
    }
}
