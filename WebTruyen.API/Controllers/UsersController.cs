﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebTruyen.API.Repository.User;
using WebTruyen.API.Service;
using WebTruyen.Library.Data;
using WebTruyen.Library.Entities;
using WebTruyen.Library.Entities.Request;
using WebTruyen.Library.Entities.ViewModel;

namespace WebTruyen.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _user;
        private readonly IStorageService _storage;

        public UsersController(IUserService context, IStorageService storage)
        {
            _user = context;
            _storage = storage;
        }

        [HttpPost("authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate(LoginRequest request)
        {
            //_storage.FileExists("thuan");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _user.Authenticate(request);

            if (string.IsNullOrEmpty(result))
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        // GET: api/Users/CheckAuthenticate
        [HttpGet("CheckAuthenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate()
        {
            var check = User.Identity.IsAuthenticated;
            if (check) return Ok("User xác thực");
            return NoContent();
        }

        // GET: api/Users
        [HttpPost("GetUserByAccessToken")]
        public async Task<ActionResult<UserVM>> GetUserByAccessToken([FromBody] string accessToken)
        {
            var user = await _user.GetUserFromAccessToken(accessToken);

            if (user != null)
            {
                return user;
            }

            return null;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserVM>>> GetUsers()
        {
            if (HttpContext.User.Identity is ClaimsIdentity identity)
            {
                IEnumerable<Claim> claims = identity.Claims;

            }
            return Ok(await _user.GetUsers());
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserVM>> GetUser(Guid id)
        {
            var user = await _user.GetUser(id);

            if (user == null)
                return NotFound();

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> PutUser(Guid id, [FromForm]UserRequest user)
        {

            if (id != user.Id)
            {
                return BadRequest();
            }

            var result = await _user.PutUser(id, user);
            if (!result)
                return NotFound();

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<UserVM>> PostUser([FromForm]UserRequest user)
        {
            if (user.Password != user.ConfirmPassword)
                return BadRequest();

            var result = await _user.PostUser(user);
            if (result.user == null)
                return Conflict(result.mess);

            return CreatedAtAction("GetUser", new { id = result.user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var result = await _user.DeleteUser(id);
            return result switch
            {
                StatusCodes.Status404NotFound => NotFound(),
                StatusCodes.Status500InternalServerError => StatusCode(500),
                _ => NoContent()
            };
        }


    }
}
