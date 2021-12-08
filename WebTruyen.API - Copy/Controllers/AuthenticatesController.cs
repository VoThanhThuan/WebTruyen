using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebTruyen.API.Repository.User;
using WebTruyen.Library.Entities.Request;

namespace WebTruyen.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticatesController : ControllerBase
    {

        private readonly IUserService _user;
        private readonly SignInManager<Library.Entities.User> _signInManager; //Thư viên đăng nhập
        private readonly IConfiguration _config; //lấy config từ appsetting.config

        public AuthenticatesController(IUserService context, SignInManager<Library.Entities.User> signInManager, IConfiguration config)
        {
            _user = context;
            _signInManager = signInManager;
            _config = config;
        }

        [HttpPost("Local")]
        public async Task<IActionResult> Authenticate(LoginRequest request)
        {
            //_storage.FileExists("thuan");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _user.Authenticate(request);

            if (string.IsNullOrEmpty(result)) {
                return BadRequest(result);
            }
            return Ok(result);
        }

        // GET: https://localhost:5001/api/Authenticates/signin-google
        [HttpGet("signin-google")]
        public async Task<IActionResult> Check()
        {
            return Ok("Ok Ok");
        }
        [HttpGet("GoogleRespone")]
        public async Task<IActionResult> CheckSignIn()
        {
            var result = await HttpContext.AuthenticateAsync("Bearer");
            return Ok("Đăng nhập thành công");
        }

        // GET: https://localhost:5001/api/Authenticates/Google
        [HttpGet("Google")]
        public async Task<IActionResult> GoogleAuthenticate(string provider = "", string returnUrl = null)
        {
            var properties = new AuthenticationProperties {
                RedirectUri = "/api/Authenticates/GoogleRespone"
            };
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }
    }
}
