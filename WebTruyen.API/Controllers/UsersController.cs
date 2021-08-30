using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebTruyen.API.Repository.User;
using WebTruyen.Library.Data;
using WebTruyen.Library.Entities.Request;
using WebTruyen.Library.Entities.ViewModel;

namespace WebTruyen.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _user;

        public UsersController(IUserService context)
        {
            _user = context;
        }

        [HttpPost("authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromForm]LoginRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _user.Authenticate(request);

            if (string.IsNullOrEmpty(result))
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        // GET: api/Users
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<UserVM>>> GetUsers()
        {
            return Ok(await _user.GetUsers());
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        [AllowAnonymous]
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
            if (result == null)
                return Conflict("Username đã tồn tại");

            return CreatedAtAction("GetUser", new { id = result.Id }, user);
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
