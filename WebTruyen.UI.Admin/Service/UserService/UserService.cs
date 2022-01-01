using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using SixLabors.ImageSharp;
using WebTruyen.Library.Entities.Request;
using WebTruyen.Library.Entities.ApiModel;
using WebTruyen.UI.Admin.RequestClient;

namespace WebTruyen.UI.Admin.Service.UserService
{
    public class UserService : IUserService
    {
        private HttpClient _http;
        ProtectedLocalStorage _localStorageService { get; set; }

        public UserService(HttpClient http, ProtectedLocalStorage localStorageService)
        {
            _http = http;
            _localStorageService = localStorageService;

        }

        private async Task GetSession()
        {
            var sessions = (await _localStorageService.GetAsync<string>("Token")).Value;
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
        }

        public async Task<string> Authenticate(LoginRequest request)
        {
            var json = JsonSerializer.Serialize(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _http.PostAsync(@"/api/Users/authenticate", httpContent);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            return null;
        }

        public async Task<UserAM> GetUserByAccessTokenAsync(string accessToken)
        {
            var json = JsonSerializer.Serialize(accessToken);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _http.PostAsync(@"/api/Users/GetUserByAccessToken", httpContent);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<UserAM>();
            }

            return null;
        }

        public async Task<List<UserAM>> GetUsers()
        {
            await GetSession();
            var result = await _http.GetFromJsonAsync<List<UserAM>>("/api/Users");
            var users = result?.Select(x => { x.Avatar = $"{_http.BaseAddress}{x.Avatar}"; return x; }).ToList();
            return users;
        }

        public async Task<UserAM> GetUser(Guid id)
        {
            await GetSession();
            var result = await _http.GetFromJsonAsync<UserAM>($"/api/Users/{id}");
            if (result == null) return null;
            result.Avatar = $"{_http.BaseAddress}{result.Avatar}";
            return result;

        }

        public async Task<int> PutUser(Guid id, UserRequestClient request)
        {
            await GetSession();
            var requestContent = new MultipartFormDataContent();

            if (!string.IsNullOrEmpty(request.Avatar.data))
            {

                var offset = request.Avatar.data.IndexOf(',') + 1;
                var data = Convert.FromBase64String(request.Avatar.data[offset..^0]);
                var bytes = new ByteArrayContent(data);
                requestContent.Add(bytes, "Avatar", request.Avatar.filename);
            }

            requestContent.Add(new StringContent(id.ToString()), "Id");
            requestContent.Add(new StringContent(request.Nickname), "Nickname");
            requestContent.Add(new StringContent(request.Username), "Username");
            requestContent.Add(new StringContent(request.Password), "Password");
            requestContent.Add(new StringContent(request.ConfirmPassword), "ConfirmPassword");
            requestContent.Add(new StringContent(request.sex.ToString()), "sex");
            requestContent.Add(new StringContent(request.Dob.ToString()), "Dob");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Address) ? "": request.Address), "Address");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.PhoneNumber) ? "" : request.PhoneNumber), "PhoneNumber");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Email) ?"": request.Email), "Email");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Fanpage) ?"":request.Fanpage), "Fanpage");
            requestContent.Add(new StringContent(request.IdRole.ToString()), "IdRole");


            var response = await _http.PutAsync($"/api/Users/{id}", requestContent);
            return (int)response.StatusCode;
        }

        public async Task<(HttpStatusCode StatusCode, UserAM)> PostUser(UserRequestClient request)
        {
            await GetSession();
            var requestContent = new MultipartFormDataContent();

            if (!string.IsNullOrEmpty(request.Avatar.data))
            {
                //await using var stream = new MemoryStream();
                //await ImageExtensions.SaveAsync(request.Avatar.data, stream, request.Avatar.format);
                //await request.Avatar.image.SaveAsync(stream, request.Avatar.format);

                //stream.TryGetBuffer(out var buffer);
                //var bytes = new ByteArrayContent(buffer.Array ?? Array.Empty<byte>());
                //requestContent.Add(bytes, "Avatar", request.Avatar.filename);

                //await stream.DisposeAsync();

                var offset = request.Avatar.data.IndexOf(',') + 1;
                var data = Convert.FromBase64String(request.Avatar.data[offset..^0]);
                var bytes = new ByteArrayContent(data);
                requestContent.Add(bytes, "Avatar", request.Avatar.filename);
            }

            requestContent.Add(new StringContent(request.Nickname), "Nickname");
            requestContent.Add(new StringContent(request.Username), "Username");
            requestContent.Add(new StringContent(request.Password), "Password");
            requestContent.Add(new StringContent(request.ConfirmPassword), "ConfirmPassword");
            requestContent.Add(new StringContent(request.sex.ToString()), "sex");
            requestContent.Add(new StringContent(request.Dob.ToString()), "Dob");
            requestContent.Add(new StringContent(request.Address), "Address");
            requestContent.Add(new StringContent(request.PhoneNumber), "PhoneNumber");
            requestContent.Add(new StringContent(request.Email), "Email");
            requestContent.Add(new StringContent(request.Fanpage), "Fanpage");
            requestContent.Add(new StringContent(request.IdRole.ToString()), "IdRole");


            var response = await _http.PostAsync($"/api/Users/", requestContent);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return (response.StatusCode, await response.Content.ReadFromJsonAsync<UserAM>());

            }

            return (response.StatusCode, null);
        }

        public async Task<int> DeleteUser(Guid id)
        {
            var response = await _http.DeleteAsync($"/api/Users/{id}");
            if (response.StatusCode == HttpStatusCode.OK) {
                return (int)response.StatusCode;

            }

            return (int)response.StatusCode;
        }
    }
}
