using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebTruyen.UI.Client.Authentication
{
    public class AuthStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly IJSRuntime JS;
        private AuthenticationState _anonymous { get; set; } = new AuthenticationState(new ClaimsPrincipal());
        public AuthStateProvider(HttpClient httpClient, ILocalStorageService localStorage, IJSRuntime js)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            JS = js;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            //var token = await _localStorage.GetItemAsync<string>("authToken");
            var token = await JS.InvokeAsync<string>("blazorExtensions.ReadCookie", "authToken");
            if (string.IsNullOrWhiteSpace(token)) {
                return _anonymous;
            }
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

            return new AuthenticationState(
                new ClaimsPrincipal(
                    new ClaimsIdentity(JwtParser.ParseClaimsFromJWT(token), "jwtAuthType")
                    )
                );
        }

        public void NotifyUserAuthentication(string token)
        {
            var authenticateUser = new ClaimsPrincipal(
                new ClaimsIdentity(JwtParser.ParseClaimsFromJWT(token), "jwtAuthType")
                );
            var authState = Task.FromResult(new AuthenticationState(authenticateUser));
            NotifyAuthenticationStateChanged(authState);
        }

        public void NotifyUserLogout()
        {
            var authState = Task.FromResult(_anonymous);
            NotifyAuthenticationStateChanged(authState);
        }

    }
}
