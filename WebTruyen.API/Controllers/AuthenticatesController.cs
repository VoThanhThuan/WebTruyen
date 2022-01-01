using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebTruyen.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticatesController : ControllerBase
    {

        private readonly IConfiguration _config; //lấy config từ appsetting.config

        public AuthenticatesController(IConfiguration config)
        {

            _config = config;
        }

        //// GET: https://localhost:5001/api/Authenticates/signin-google
        //[HttpGet("signin-google")]
        //public async Task<IActionResult> Check()
        //{
        //    return Ok("Ok Ok");
        //}
        //[HttpGet("GoogleRespone")]
        //public async Task<IActionResult> CheckSignIn()
        //{
        //    var result = await HttpContext.AuthenticateAsync("Bearer");
        //    var claims = result.Principal.Identities.FirstOrDefault()
        //        .Claims.Select(claim => new {
        //            claim.Issuer,
        //            claim.Type,
        //            claim.Value,
        //            claim.Properties
        //        });
        //    string jsonString = JsonSerializer.Serialize(claims);
        //    return Ok(jsonString);
        //}

        //// GET: https://localhost:5001/api/Authenticates/Google
        //[HttpGet("Google")]
        //public async Task<IActionResult> GoogleAuthenticate(string provider = "", string returnUrl = null)
        //{
        //    var properties = new AuthenticationProperties {
        //        RedirectUri = "/api/Authenticates/GoogleRespone"
        //    };
        //    return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        //}
    }
}
