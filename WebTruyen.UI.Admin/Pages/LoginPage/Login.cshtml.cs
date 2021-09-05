using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using WebTruyen.Library.Entities.Request;
using WebTruyen.UI.Admin.Data.Constants;
using WebTruyen.UI.Admin.Service.UserService;

namespace WebTruyen.UI.Admin.Pages.LoginPage
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private IUserService _userApi { get; set; }
        private IConfiguration _configuration { get; set; }

        public LoginModel(IUserService userApi, IConfiguration configuration)
        {
            _userApi = userApi;
            _configuration = configuration;
        }

        private SystemConstants scs;

        public string ReturnUrl { get; set; }
        public async Task<IActionResult>
            OnGetAsync(string paramUsername, string paramPassword)
        {
            var loginRequest = new LoginRequest() {Username = paramUsername, Password = paramPassword, RememberMe = true};
            var token = await _userApi.Authenticate(loginRequest);

            if (token == null)
            {
                //ModelState.AddModelError("", token.Message);
                return null;
            }

            var userPrincipal = this.ValidateToken(token);

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                IsPersistent = true //xác thực mỗi lần mở lại browser
            };
            HttpContext.Session.SetString(scs.Token, token);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                userPrincipal,
                authProperties);

            string returnUrl = Url.Content("~/");

            //try
            //{
            //    // Clear the existing external cookie
            //    await HttpContext
            //        .SignOutAsync(
            //            CookieAuthenticationDefaults.AuthenticationScheme);
            //}
            //catch { }
            // *** !!! This is where you would validate the user !!! ***
            // In this example we just log the user in
            // (Always log the user in for this demo)
            //var claims = new List<Claim>
            //{
            //    new Claim(ClaimTypes.Name, loginRequest.Username),
            //    new Claim(ClaimTypes.Role, "Administrator"),
            //};
            //var claimsIdentity = new ClaimsIdentity(
            //    claims, CookieAuthenticationDefaults.AuthenticationScheme);
            //var authProperties = new AuthenticationProperties
            //{
            //    IsPersistent = true,
            //    RedirectUri = this.Request.Host.Value
            //};
            //try
            //{
            //    await HttpContext.SignInAsync(
            //        CookieAuthenticationDefaults.AuthenticationScheme,
            //        new ClaimsPrincipal(claimsIdentity),
            //        authProperties);
            //}
            //catch (Exception ex)
            //{
            //    string error = ex.Message;
            //}
            return LocalRedirect(returnUrl);
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
    }
}
