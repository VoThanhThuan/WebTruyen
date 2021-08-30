using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebTruyen.Library.Entities.Request;
using WebTruyen.Library.Entities.ViewModel;
using WebTruyen.UI.Admin.RequestClient;

namespace WebTruyen.UI.Admin.Service.UserService
{
    public class UserService : IUserService
    {
        private readonly HttpClient _http;

        public UserService(HttpClient http)
        {
            _http = http;
        }
        public async Task<List<UserVM>> GetUsers()
        {
            var result = await _http.GetFromJsonAsync<List<UserVM>>("/api/Users");
            var users = result?.Select(x => { x.Avatar = $"{_http.BaseAddress}{x.Avatar}"; return x; }).ToList();
            return users;
        }

        public Task<string> Authenticate(LoginRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<UserVM> GetUser(Guid id)
        {
            var result = await _http.GetFromJsonAsync<UserVM>($"/api/Users/{id}");
            if (result != null)
            {
                result.Avatar = $"{_http.BaseAddress}{result.Avatar}";
                return result;
            }

            return null;
        }

        public Task<bool> PutUser(Guid id, UserRequestClient request)
        {
            throw new NotImplementedException();
        }

        public async Task<(HttpStatusCode StatusCode, UserVM)> PostUser(UserRequestClient request)
        {
            var requestContent = new MultipartFormDataContent();

            if (request.Avatar != null)
            {
                var data = new byte[request.Avatar.Size];
                await using (var br = request.Avatar.OpenReadStream())
                {
                    await br.ReadAsync(data);
                }

                var bytes = new ByteArrayContent(data);
                requestContent.Add(bytes, "Avatar", request.Avatar.Name);
            }

            requestContent.Add(new StringContent(request.Nickname), "Nickname");
            requestContent.Add(new StringContent(request.Username), "Username");
            requestContent.Add(new StringContent(request.Password), "Password");
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
                return (response.StatusCode, await response.Content.ReadFromJsonAsync<UserVM>());

            }
            else
            {
                return (response.StatusCode, null);

            }
        }

        public Task<int> DeleteUser(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
