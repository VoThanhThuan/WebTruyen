﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace WebTruyen.UI.Admin.Service.ImageService
{
    public class ImageService : IImageService
    {
        private readonly HttpClient _http;
        ProtectedLocalStorage _localStorageService { get; set; }

        public ImageService(HttpClient http, ProtectedLocalStorage localStorageService)
        {
            _http = http;
            _localStorageService = localStorageService;
        }

        private async void GetSession()
        {
            var sessions = (await _localStorageService.GetAsync<string>("Token")).Value;
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
            await using var br = img.OpenReadStream(maxAllowedSize: 2048000L);
            await br.ReadAsync(buffer); //ghi dữ liệu vào bộ nhớ đệm
            GC.Collect();
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
            if (url == _http.BaseAddress.ToString()) return "";
            GetSession();
            Console.WriteLine($">>> GetImageFromUrl: {url}");
            var result = await _http.GetByteArrayAsync(url);
            return ByteToString(result);
        }

        public async IAsyncEnumerable<byte[]> ImagesToByte(IReadOnlyList<IBrowserFile> imgs)
        {
            foreach (var img in imgs) {
                var data = new byte[img.Size];
                await using var br = img.OpenReadStream();
                await br.ReadAsync(data);
                yield return data;
            }
            GC.Collect();
        }

        public async IAsyncEnumerable<((byte[] data, string fileName, string contentType) image, string stringValue)> ImagesToString(
            IReadOnlyList<IBrowserFile> imgs)
        {
            foreach (var img in imgs) {
                if (img.Size > 2048000L)
                    yield return (imge: (data: null, fileName: null, contentType: null), stringValue: null);
                var buffer = new byte[img.Size];
                await using var br = img.OpenReadStream(maxAllowedSize: 2048000L);
                await br.ReadAsync(buffer);
                br.Close();
                var contentType = img.ContentType; //lấy định dạng file

                var dataString = $"data:{contentType};base64,{Convert.ToBase64String(buffer)}";

                var value = (imge: (data: buffer, fileName: img.Name, contentType), stringValue: dataString);

                yield return value;
            }
            GC.Collect();
        }
    }
}
