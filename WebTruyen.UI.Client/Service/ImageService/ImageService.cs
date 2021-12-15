using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.WebAssembly.Http;

namespace WebTruyen.UI.Client.Service.ImageService
{
    public class ImageService : IImageService
    {
        private readonly HttpClient _http;
        private readonly ISessionStorageService _sessionStorage;

        public ImageService(HttpClient http, ISessionStorageService sessionStorage)
        {
            _http = http;
            _sessionStorage = sessionStorage;

        }

        private async void GetSession()
        {
            var sessions = (await _sessionStorage.GetItemAsync<string>("Token"));

            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
        }

        public bool ImageIsValid(string contentType)
        {
            var fileTypeSupported = new List<string>() { "image/jpg", "image/jpeg", "image/png", "image/gif", "image/bmp" };
            return fileTypeSupported.Any(type => contentType == type);
        }

        public async Task<byte[]> ImageToByte(IBrowserFile img)
        {
            var buffer = new byte[img.Size]; // Tạo bộ nhớ đệm
            Console.WriteLine($"ImageService > ImageToByte > buffer");
            await using var br = img.OpenReadStream();
            await br.ReadAsync(buffer); //ghi dữ liệu vào bộ nhớ đệm
            Console.WriteLine($"ImageService > ImageToByte > await br.ReadAsync(buffer);");

            return buffer;
        }

        public async Task<string> ImageToString(IBrowserFile img)
        {
            var format = img.ContentType; //lấy định dạng file

            return $"data:{format};base64,{Convert.ToBase64String(await ImageToByte(img))}";
        }

        public string ByteToString(byte[] value)
        {

            return $"data:image/jpg;base64,{Convert.ToBase64String(value)}";
        }

        public async Task<string> GetImageFromUrl(string url)
        {
            GetSession();

            var content = new StringContent(url);
            var result = await _http.PostAsync(url, content);
            //Console.WriteLine($">>> GetImageFromUrl: reusult: {result}");
            if (result.IsSuccessStatusCode)
            {
                return ByteToString(await result.Content.ReadAsByteArrayAsync());
            }
            //var result = await _http.GetByteArrayAsync(url);
            //return "./resources/img/Psyduck-image-404.png";
            return url;
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

        public async IAsyncEnumerable<((byte[] data, string fileName) image, string stringValue)> ImagesToString(
            IReadOnlyList<IBrowserFile> imgs)
        {
            foreach (var img in imgs)
            {
                if (img.Size > 2048000L)
                    yield return (imge: (data: null, fileName: null), stringValue: null);
                var buffer = new byte[img.Size];
                await using var br = img.OpenReadStream(maxAllowedSize: 2048000L);
                await br.ReadAsync(buffer);
                br.Close();
                var format = img.ContentType; //lấy định dạng file

                var dataString = $"data:{format};base64,{Convert.ToBase64String(buffer)}";

                var value = (imge: (data: buffer, fileName: img.Name), stringValue: dataString);

                yield return value;
            }
        }
    }
}
