using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace WebTruyen.API.Service
{
    public class WaterMarkService : IWaterMarkService
    {

        public WaterMarkService(IWebHostEnvironment webHostEnvironment)
        {
            _userContentFolder = Path.Combine(webHostEnvironment.WebRootPath);
            _securityContentFolder = Path.Combine($"{webHostEnvironment.ContentRootPath}\\MyStaticFiles");

        }

        private readonly string _userContentFolder;
        private readonly string _securityContentFolder;

        public async Task<string> SaveImage(IFormFile file, string path, bool security = false)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim().Value;
            var fileName = $@"{path}/{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            var filePath = security == false ? $"{_userContentFolder}/{fileName}" : $"{_securityContentFolder}/{fileName}";

            var stream = file.OpenReadStream();
            using var image = await Image.LoadAsync(stream);
            image.Mutate(
                context => {
                    const string text = "Võ Thành Thuận - DPM185194 - Đồ Án Môn Học";
                    var font = SystemFonts.CreateFont("Arial", 26);

                    context.DrawText(
                        text,
                        font,
                        Rgba32.ParseHex("#5e72e45c"),
                        new PointF(20, image.Height-30));
                });
            await image.SaveAsync(filePath);
            return fileName;
        }
    }
}
