using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using WebTruyen.Library.Entities.Request;
using WebTruyen.Library.Entities.ViewModel;
using WebTruyen.UI.Admin.Service.UserService;

namespace WebTruyen.UI.Admin.Data
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {

        [Inject] ProtectedLocalStorage _localStorageService { get; set; }
        public IUserService _userService { get; set; }

        private readonly IConfiguration _configuration;

        private HttpClient _http;

        public CustomAuthenticationStateProvider(ProtectedLocalStorage localStorageService,
            IUserService userService,
            HttpClient httpClient,
            IConfiguration configuration)
        {
            //throw new Exception("CustomAuthenticationStateProviderException");
            _localStorageService = localStorageService;
            _userService = userService;
            _http = httpClient;
            _configuration = configuration;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var accessToken = await _localStorageService.GetAsync<string>("Token");

                ClaimsPrincipal identity;

                if (!string.IsNullOrEmpty(accessToken.Value))
                {
                    identity = ValidateToken(accessToken.Value);
                }
                else
                {
                    identity = new ClaimsPrincipal();
                }

                return new AuthenticationState(identity);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return new AuthenticationState(new ClaimsPrincipal());
        }

        public async Task MarkUserAsAuthenticated(string token)
        {
            await _localStorageService.SetAsync("Token", token);
           // await _localStorageService.SetItemAsync("refreshToken", user.RefreshToken);

            var identity = ValidateToken(token);

            var claimsPrincipal = new ClaimsPrincipal(identity);

            var sessions = (await _localStorageService.GetAsync<string>("Token")).Value;

            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);


            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }

        public async Task MarkUserAsLoggedOut()
        {
            //await _localStorageService.DeleteAsync("Token");
            await _localStorageService.DeleteAsync("Token");

            var identity = new ClaimsIdentity();

            var user = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }
        private ClaimsPrincipal ValidateToken(string jwtToken)
        {
            IdentityModelEventSource.ShowPII = true;


            var validationParameters = new TokenValidationParameters
            {
                ValidateLifetime = true,
                ValidIssuer = _configuration["Tokens:Issuer"],
                ValidAudience = _configuration["Tokens:Issuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]))
            };

            var principal = new JwtSecurityTokenHandler().ValidateToken(jwtToken, validationParameters, out SecurityToken validatedToken);

            return principal;
        }

        //public async Task<UserVM> GetUserByAccessTokenAsync(string accessToken)
        //{
        //    string serializedRefreshRequest = JsonSerializer.Serialize(accessToken);

        //    var requestMessage = new HttpRequestMessage(HttpMethod.Post, "Users/GetUserByAccessToken");
        //    requestMessage.Content = new StringContent(serializedRefreshRequest);

        //    requestMessage.Content.Headers.ContentType
        //        = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

        //    var response = await _httpClient.SendAsync(requestMessage);

        //    var responseStatusCode = response.StatusCode;
        //    var responseBody = await response.Content.ReadAsStringAsync();

        //    var returnedUser = JsonSerializer.Deserialize<UserVM>(responseBody);

        //    return await Task.FromResult(returnedUser);
        //}
    }
}
