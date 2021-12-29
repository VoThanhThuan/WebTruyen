using Blazored.LocalStorage;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using WebTruyen.Library.Entities.ApiModel;
using WebTruyen.Library.Entities.Request;
using WebTruyen.UI.Client.Authentication;
using WebTruyen.UI.Client.Service.GenreService;
using WebTruyen.UI.Client.Service.UserService;

namespace WebTruyen.UI.Client.Shared
{
    public partial class NavMenu
    {
        #region initialization


        public Element _element { get; set; } = new();

        public LoginRequest LoginRequest { get; set; } = new();

        [Inject] IUserApiClient _userApi { get; set; }
        [Inject] ISessionStorageService _sessionStorage { get; set; }
        [Inject] ILocalStorageService _localStorage { get; set; }
        [Inject] IGenreApiClient _genreApi { get; set; }

        [Inject] NavigationManager _navigationManager { get; set; }
        [Inject] IJSRuntime JS { get; set; }
        [Inject] AuthenticationStateProvider _authStateProivder { get; set; }

        [Inject] HttpClient _http { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await GetGenre();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender) {
                var context = await _authStateProivder.GetAuthenticationStateAsync();
                //var id = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

                //foreach(var i in context.User.Claims) {
                //    Console.WriteLine($"NavMenu >> User>> {i.Type} : {i.Value}");
                //}
                //var token = await _localStorage.GetItemAsStringAsync("Token");
                var token = await JS.InvokeAsync<string>("blazorExtensions.ReadCookie", "Token");
                var isdark = await _localStorage.GetItemAsStringAsync("dark");
                OnDarkMode(isdark != "1");
                if (!string.IsNullOrEmpty(token)) {
                    await GetUserByToken(token);
                    StateHasChanged();
                } else {
                    await SignOut();
                }
                FuncCode.SignOut = () => {
                    _userApi.Logout();
                    JS.InvokeVoidAsync("blazorExtensions.DeleteCookie", "Token");
                    _sessionStorage.ClearAsync();
                    //_localStorage.ClearAsync();
                    _element.IsSignIn = false;
                    _element.User = new UserAM();
                    _navigationManager.NavigateTo("/");
                    StateHasChanged();
                };
            }
        }

        #endregion



        #region Xử lý giao diện
        void OnDarkMode(bool isDark)
        {
            _element.OnDark = !isDark;
            _localStorage.SetItemAsStringAsync("dark", _element.OnDark ? "1" : "0");
            StateHasChanged();
        }

        void OpenGenreMenu()
        {
            _element.pop_grene = !_element.pop_grene;
            BackgroundClick();
            StateHasChanged();
        }
        void OpenUserMenu()
        {
            _element.pop_user = !_element.pop_user;
            BackgroundClick();
            StateHasChanged();
        }
        void OpenNotificationMenu()
        {
            _element.pop_notification = !_element.pop_notification;
            BackgroundClick();
            StateHasChanged();
        }

        void BackgroundClick()
        {
            if (!_element.pop_grene && !_element.pop_user) {
                _element.pop_background_click = false;
            } else {
                _element.pop_background_click = true;
            }
        }

        void ClosePopMenu()
        {
            _element.pop_background_click = false;
            _element.pop_grene = false;
            _element.pop_user = false;
        }

        void HambugerClick()
        {
            if(_element.Active == "") {
                _element.Active = "active";
            } else {
                _element.Active = "";
            }
        }

        #endregion

        #region Xử lý dữ liệu

        void SignIn()
        {
            var uri = _navigationManager.Uri;
            var baseUri = _navigationManager.BaseUri;
            var returnUrl = uri.Replace(baseUri, "");
            _navigationManager.NavigateTo($"/login?ReturnUrl=/{returnUrl}");
        }

        async Task SignOut()
        {
            await _userApi.Logout();
            await JS.InvokeVoidAsync("blazorExtensions.DeleteCookie", "Token");
            await _sessionStorage.ClearAsync();
            //_localStorage.ClearAsync();
            _element.IsSignIn = false;
            _element.User = new UserAM();
            StateHasChanged();
        }

        void SearchText()
        {
            if(string.IsNullOrEmpty(_element.TextSearch)) {
                _navigationManager.NavigateTo($"/");

            } else {
                _navigationManager.NavigateTo($"/search/{_element.TextSearch}");
            }
            StateHasChanged();
        }

        //private async Task OnInputChange(ChangeEventArgs args)
        //{
            
        //}

        #endregion

        #region Giao tiếp server

        //async Task SignIn(EditContext editContext)
        //{
        //    var token = await _userApi.Authenticate(LoginRequest);
        //    if (string.IsNullOrEmpty(token)) {
        //        Console.WriteLine($">>> [❌] Đăng thập thất bại ");
        //    } else {
        //        Console.WriteLine($">>> [⭕] Đăng Nhập Thành Công");

        //        await _localStorage.SetItemAsStringAsync("Token", token);
        //        await _sessionStorage.SetItemAsStringAsync("Token", token);
        //        //await GetUserByToken(token);
        //        StateHasChanged();

        //        _navigationManager.NavigateTo(_navigationManager.Uri, forceLoad: true);
        //    }
        //}

        async Task GetUserByToken(string token)
        {
            var user = await _userApi.GetUserByAccessTokenAsync(token);
            Console.WriteLine($"NavMenu > GetUserByToken > user: {user.Id}");
            if (user is not null) {
                //Console.WriteLine($"NavMenu > GetUserByToken > user.Id: {user.Id}");
                //Console.WriteLine($"NavMenu > GetUserByToken > user.Avatar: {user.Avatar}");
                _element.IsSignIn = true;
                _element.User = user;

                _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var jsonUser = JsonSerializer.Serialize(user);
                
                //await _localStorage.SetItemAsStringAsync("Token", token);
                await JS.InvokeAsync<string>("blazorExtensions.WriteCookie", "Token", token, 1);
                await _sessionStorage.SetItemAsStringAsync("Token", token);
                await _sessionStorage.SetItemAsStringAsync("user_webtruyen", jsonUser);
            } else {
                await JS.InvokeVoidAsync("blazorExtensions.DeleteCookie", "Token");
                await _localStorage.RemoveItemAsync("Token");
                ((AuthStateProvider)_authStateProivder).NotifyUserLogout();

            }
            StateHasChanged();
        }

        async Task GetGenre()
        {
            var genres = await _genreApi.GetGenres();
            if (genres is not null) {
                _element.Genres = genres;
                StateHasChanged();
            }
        }

        void SearchGenre(int id)
        {
            _element.pop_grene = !_element.pop_grene;
            BackgroundClick();
            StateHasChanged();
            _navigationManager.NavigateTo($"/genre/{id}");
        }

        #endregion



        public class Element
        {
            public bool OnDark { get; set; } = false;
            public string Active { get; set; } = "";
            public bool IsSignIn { get; set; } = false;
            public List<ComicAM> ComicSearch { get; set; } = new List<ComicAM>();
            public string TextSearch { get; set; } = "";
            public bool pop_grene { get; set; } = false;
            public bool pop_search { get; set; } = false;
            public bool pop_user { get; set; } = false;
            public bool pop_notification { get; set; } = false;
            public bool pop_background_click { get; set; } = false;

            public UserAM User { get; set; } = new UserAM();
            public List<GenreAM> Genres { get; set; } = new List<GenreAM>();

        }
    }
}
