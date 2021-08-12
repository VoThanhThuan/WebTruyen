using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;

namespace WebTruyen.UI.Admin.Service.ImageService
{
    public class ImageService : IImageService
    {
        public bool ImageIsValid(string contentType)
        {
            var fileTypeSupported = new List<string>() { "image/jpg", "image/jpeg", "image/png", "image/gif", "image/webp", "image/bmp", "image/tiff" };
            return fileTypeSupported.Any(type => contentType == type);
        }

        public async Task<byte[]> ImageToByte(IBrowserFile img)
        {

            var buffer = new byte[img.Size]; // Tạo bộ nhớ đệm
            await using var br = img.OpenReadStream();
            await br.ReadAsync(buffer); //ghi dữ liệu vào bộ nhớ đệm

            return buffer;
        }

        public async Task<string> ImageToString(IBrowserFile img)
        {
            var format = img.ContentType; //lấy định dạng file

            return $"data:{format};base64,{Convert.ToBase64String(await ImageToByte(img))}";
        }

        public async IAsyncEnumerable<byte[]> ImagesToByte(IReadOnlyList<IBrowserFile> imgs)
        {
            foreach (var img in imgs)
            {
                var data = new byte[img.Size];
                await using var br = img.OpenReadStream();
                await br.ReadAsync(data);
                yield return data;
            }
        }

        public async IAsyncEnumerable<(byte[] data, string stringValue)> ImagesToString(IReadOnlyList<IBrowserFile> imgs)
        {
            foreach (var img in imgs)
            {
                if (img.Size > 2048000L)
                    yield return (data: null, stringValue: null);
                var buffer = new byte[img.Size];
                await using var br = img.OpenReadStream(maxAllowedSize: 2048000L);
                await br.ReadAsync(buffer);
                br.Close();
                var format = img.ContentType; //lấy định dạng file

                var dataString = $"data:{format};base64,{Convert.ToBase64String(buffer)}";

                var value = (data: buffer, stringValue: dataString);

                yield return value;
            }

        }
    }
}
