using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebTruyen.API.Repository.Role;
using WebTruyen.Library.Data;
using WebTruyen.Library.Entities;

namespace WebTruyen.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _role;

        public RolesController(IRoleService context)
        {
            _role = context;
        }

        // GET: api/Roles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role>>> GetRoles()
        {
            return Ok(await _role.GetRoles());
        }

        // GET: api/Roles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Role>> GetRole(Guid id)
        {
            var role = await _role.GetRole(id);

            if (role == null)
            {
                return NotFound();
            }

            return role;
        }

        // PUT: api/Roles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRole(Guid id, Role role)
        {
            if (id != role.Id)
            {
                return BadRequest();
            }

            var result = await _role.PutRole(id, role);
            if (!result)
                return NotFound();

            return NoContent();
        }

        // POST: api/Roles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Role>> PostRole(Role role)
        {
            await _role.PostRole(role);

            return CreatedAtAction("GetRole", new { id = role.Id }, role);
        }

        // DELETE: api/Roles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(Guid id)
        {
            var result = await _role.DeleteRole(id);
            if (!result)
                return NotFound();

            return NoContent();
        }

    }
}
