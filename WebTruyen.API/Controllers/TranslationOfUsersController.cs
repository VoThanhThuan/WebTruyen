using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebTruyen.API.Repository.TranslationOfUser;
using WebTruyen.Library.Data;
using WebTruyen.Library.Entities.ViewModel;

namespace WebTruyen.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TranslationOfUsersController : ControllerBase
    {
        private readonly ITranslationOfUserService _translation;

        public TranslationOfUsersController(ITranslationOfUserService context)
        {
            _translation = context;
        }

        // GET: api/TranslationOfUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TranslationOfUserVM>>> GetTranslationOfUsers()
        {
            return Ok(await _translation.GetTranslationOfUsers());
        }

        // GET: api/TranslationOfUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TranslationOfUserVM>> GetTranslationOfUser(Guid id)
        {
            var translationOfUser = await _translation.GetTranslationOfUser(id);

            if (translationOfUser == null)
                return NotFound();

            return translationOfUser;
        }

        // PUT: api/TranslationOfUsers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTranslationOfUser(Guid id, TranslationOfUserVM translationOfUser)
        {
            if (id != translationOfUser.IdUser)
            {
                return BadRequest();
            }

            var result = await _translation.PutTranslationOfUser(id, translationOfUser);
            if (!result)
                return NotFound();

            return NoContent();
        }

        // POST: api/TranslationOfUsers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TranslationOfUserVM>> PostTranslationOfUser(TranslationOfUserVM translationOfUser)
        {
            var result = await _translation.PostTranslationOfUser(translationOfUser);
            if (!result)
                return Conflict();

            return CreatedAtAction("GetTranslationOfUser", new { id = translationOfUser.IdUser }, translationOfUser);
        }

        // DELETE: api/TranslationOfUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTranslationOfUser(Guid id)
        {
            var result = await _translation.DeleteTranslationOfUser(id);
            if (!result)
                return NotFound();

            return NoContent();
        }


    }
}
