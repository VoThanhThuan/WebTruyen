using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WebTruyen.Library.Entities.ApiModel;
using WebTruyen.Library.Entities.Request;
using WebTruyen.UI.Client.Model;
using WebTruyen.UI.Client.Service.ImageService;

namespace WebTruyen.UI.Client.Service.UserService
{
    public class UserService : IUserService
    {
        private HttpClient _http;
        ISessionStorageService _sessionStorage { get; set; }
        IImageService _image { get; set; }

        public UserService(HttpClient http, ISessionStorageService sessionStorage, IImageService image)
        {
            _http = http;
            _sessionStorage = sessionStorage;
            _image = image;
        }

        private async Task GetSession()
        {
            var sessions = (await _sessionStorage.GetItemAsync<string>("Token"));
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
        }

        public async Task<string> Authenticate(LoginRequest request)
        {
            var json = JsonSerializer.Serialize(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _http.PostAsync(@"/api/Users/authenticate", httpContent);
            if (response.IsSuccessStatusCode) {
                return await response.Content.ReadAsStringAsync();
            }

            return null;
        }

        public async Task<UserAM> GetUserByAccessTokenAsync(string accessToken)
        {
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await _http.GetAsync(@"/api/Users/GetUserByAccessToken");
            if (response.StatusCode == HttpStatusCode.OK) {
                var user = await response.Content.ReadFromJsonAsync<UserAM>();
                user.Avatar = $"{_http.BaseAddress}{user.Avatar}";
                Console.WriteLine($"UserService > GetUserByAccessTokenAsync > userId: {user.Id}");
                return user;
            }

            return null;
        }

        public async Task<List<UserAM>> GetUsers()
        {
            await GetSession();
            var result = await _http.GetFromJsonAsync<List<UserAM>>($"/api/GetCommentInComic?");
            var users = result?.Select(x => { x.Avatar = $"{_http.BaseAddress}{x.Avatar}"; return x; }).ToList();
            return users;
        }

        public async Task<UserAM> GetUser(Guid id)
        {
            await GetSession();
            var result = await _http.GetFromJsonAsync<UserAM>($"/api/Users/{id}");
            if (result == null) return null;
            result.Avatar = $"{_http.BaseAddress}{result.Avatar}";
            Console.WriteLine($"UserService > GetUser > result.Id: {result.Id}");
            return result;

        }

        public async Task<(int apiResult, string mess, UserAM user)> Register(RegisterRequestClient request)
        {
            await GetSession();
            var requestContent = new MultipartFormDataContent();

            if (!string.IsNullOrEmpty(request.Avatar.data)) {

                var offset = request.Avatar.data.IndexOf(',') + 1;
                var data = Convert.FromBase64String(request.Avatar.data[offset..^0]);
                var bytes = new ByteArrayContent(data);
                requestContent.Add(bytes, "Avatar", request.Avatar.filename);
            }

            //Console.WriteLine("UserService.cs > Register");
            //Console.WriteLine($"Nickname: {request.Nickname}" );
            //Console.WriteLine($"Username: {request.Username}");
            //Console.WriteLine($"Password: {request.Password}");
            //Console.WriteLine($"ConfirmPassword: {request.ConfirmPassword}");
            //Console.WriteLine($"sex: {request.sex}");
            //Console.WriteLine($"Dob: {request.Dob.ToString("dd/MM/yyyy", DateTimeFormatInfo.InvariantInfo)}");
            //Console.WriteLine($"Address: {request.Address}");
            //Console.WriteLine($"PhoneNumber: {request.PhoneNumber}");
            //Console.WriteLine($"Email: {request.Email}");
            //Console.WriteLine($"Fanpage: {request.Fanpage}");

            requestContent.Add(new StringContent(request.Nickname), "Nickname");
            requestContent.Add(new StringContent(request.Username), "Username");
            requestContent.Add(new StringContent(request.Password), "Password");
            requestContent.Add(new StringContent(request.ConfirmPassword), "ConfirmPassword");
            requestContent.Add(new StringContent(request.sex.ToString()), "sex");
            requestContent.Add(new StringContent(request.Dob.ToString("dd/MM/yyyy")), "Dob");
            requestContent.Add(new StringContent(request.Address), "Address");
            requestContent.Add(new StringContent(request.PhoneNumber), "PhoneNumber");
            requestContent.Add(new StringContent(request.Email), "Email");
            requestContent.Add(new StringContent(request.Fanpage), "Fanpage");


            var response = await _http.PostAsync($"/api/Users/Register/", requestContent);
            if (response.StatusCode == HttpStatusCode.OK) {
                return ((int)response.StatusCode, "", await response.Content.ReadFromJsonAsync<UserAM>());

            }

            return ((int)response.StatusCode, response.RequestMessage.ToString(), null);
        }

        public async Task<(int statusCode, string mess)> UpdateInfoUser(Guid idUser, InfoUser info)
        {
            await GetSession();
            //var requestContent = new MultipartFormDataContent
            //{
            //    {new StringContent(request.Nickname), "Nickname"},
            //    {new StringContent(request.sex.ToString()), "sex"},
            //    {new StringContent(request.Dob.ToString("dd/MM/yyyy")), "Dob"},
            //    {new StringContent(request.Address), "Address"},
            //    {new StringContent(request.PhoneNumber), "PhoneNumber"},
            //    {new StringContent(request.Email), "Email"},
            //    {new StringContent(request.Fanpage), "Fanpage"}
            //};

            var json = JsonSerializer.Serialize(info);


            var response = await _http.PutAsJsonAsync($"/api/Users/UpdateInfoUser/{idUser}", json);
            if (response.StatusCode == HttpStatusCode.OK) {
                return ((int)response.StatusCode, response.RequestMessage.ToString());

            }
            return ((int)response.StatusCode, response.RequestMessage.ToString());
        }

        public async Task<(int statusCode, string mess)> UpdateAvatar(Guid idUser, IBrowserFile avatar)
        {
            await GetSession();
            var requestContent = new MultipartFormDataContent();

            if (avatar != null) {

                var data = await _image.ImageToByte(avatar);
                var bytes = new ByteArrayContent(data);
                requestContent.Add(bytes, "Avatar", avatar.Name);

                var response = await _http.PutAsync($"/api/Users/UpdateAvater/{idUser}", requestContent);
                if (response.StatusCode == HttpStatusCode.OK) {
                    return ((int)response.StatusCode, response.RequestMessage.ToString());

                }
                return ((int)response.StatusCode, response.RequestMessage.ToString());

            }
            return (400, "Avatar không được rỗng");
        }

        public async Task<(int statusCode, string mess)> UpdatePassword(Guid idUser, ChangePasswordRequest password)
        {
            await GetSession();

            var json = JsonSerializer.Serialize(password);
            var response = await _http.PutAsJsonAsync($"/api/Users/UpdatePassword/{idUser}", json);
            if (response.StatusCode == HttpStatusCode.OK) {
                return ((int)response.StatusCode, response.RequestMessage.ToString());

            }
            return ((int)response.StatusCode, response.RequestMessage.ToString());

        }
    }
}
