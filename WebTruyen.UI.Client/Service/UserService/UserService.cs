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
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using WebTruyen.Library.Entities.Request;
using WebTruyen.Library.Entities.ViewModel;

namespace WebTruyen.UI.Client.Service.UserService
{
    public class UserService : IUserService
    {
        private HttpClient _http;
        ISessionStorageService _sessionStorage { get; set; }

        public UserService(HttpClient http, ISessionStorageService sessionStorage)
        {
            _http = http;
            _sessionStorage = sessionStorage;

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
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            return null;
        }

        public async Task<UserVM> GetUserByAccessTokenAsync(string accessToken)
        {
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await _http.GetAsync(@"/api/Users/GetUserByAccessToken");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return await response.Content.ReadFromJsonAsync<UserVM>();
            }

            return null;
        }

        public async Task<List<UserVM>> GetUsers()
        {
            await GetSession();
            var result = await _http.GetFromJsonAsync<List<UserVM>>("/api/Users");
            var users = result?.Select(x => { x.Avatar = $"{_http.BaseAddress}{x.Avatar}"; return x; }).ToList();
            return users;
        }

        public async Task<UserVM> GetUser(Guid id)
        {
            await GetSession();
            var result = await _http.GetFromJsonAsync<UserVM>($"/api/Users/{id}");
            if (result == null) return null;
            result.Avatar = $"{_http.BaseAddress}{result.Avatar}";
            return result;

        }


    }
}
