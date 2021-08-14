using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;

namespace WebTruyen.API.Service
{
    public class FileService : IStorageService
    {
        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _userContentFolder = Path.Combine(webHostEnvironment.WebRootPath);
        }

        private readonly string _userContentFolder;

        public string GetFileUrl(string fileName)
        {
            return $@"/{fileName}";
        }


        private async Task SaveFileAsync(Stream mediaBinaryStream, string fileName)
        {
            var filePath = Path.Combine(_userContentFolder, fileName);
            await using var output = new FileStream(filePath, FileMode.Create, FileAccess.Write);
            await mediaBinaryStream.CopyToAsync(output);
            //output.Close();
        }

        public DirectoryInfo CreateDirectory(string path)
        {
            var newPath = Path.Combine(_userContentFolder, path);
            return Directory.CreateDirectory(newPath);
        }

        public async Task<int> DeleteFileAsync(string fileName)
        {
            var filePath = Path.Combine(_userContentFolder, fileName);
            if (!File.Exists(filePath)) return StatusCodes.Status200OK;
            try
            {
                await Task.Run(() => File.Delete(filePath));
            }
            catch (Exception)
            {
                return StatusCodes.Status500InternalServerError;
            }
            return StatusCodes.Status200OK;

        }

        public async Task<string> SaveFile(IFormFile file, string path)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim().Value;
            var fileName = $@"{path}/{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }

        public async Task<int> DeleteFolderAsync(string folder)
        {
            var di = new DirectoryInfo(folder);

            foreach (var file in di.GetFiles())
            {
                try
                {
                    await Task.Run(() => file.Delete());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return StatusCodes.Status500InternalServerError;
                }
            }
            foreach (var dir in di.GetDirectories())
            {
                try
                {
                    await Task.Run(() => dir.Delete(true));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return StatusCodes.Status500InternalServerError;
                }
            }

            return 200;
        }
    }
}
