using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebTruyen.API.Data;
using WebTruyen.API.Entities;
using WebTruyen.API.Entities.ViewModel;

namespace WebTruyen.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TranslationOfUsersController : ControllerBase
    {
        private readonly ComicDbContext _context;

        public TranslationOfUsersController(ComicDbContext context)
        {
            _context = context;
        }

        // GET: api/TranslationOfUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TranslationOfUserVM>>> GetTranslationOfUsers()
        {
            return await _context.TranslationOfUsers.Select(x => x.ToViewModel()).ToListAsync();
        }

        // GET: api/TranslationOfUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TranslationOfUserVM>> GetTranslationOfUser(Guid id)
        {
            var translationOfUser = await _context.TranslationOfUsers.FindAsync(id);

            if (translationOfUser == null)
            {
                return NotFound();
            }

            return translationOfUser.ToViewModel();
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

            _context.Entry(translationOfUser.ToTranslationOfUser()).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TranslationOfUserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TranslationOfUsers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TranslationOfUserVM>> PostTranslationOfUser(TranslationOfUserVM translationOfUser)
        {
            _context.TranslationOfUsers.Add(translationOfUser.ToTranslationOfUser());
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TranslationOfUserExists(translationOfUser.IdUser))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTranslationOfUser", new { id = translationOfUser.IdUser }, translationOfUser);
        }

        // DELETE: api/TranslationOfUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTranslationOfUser(Guid id)
        {
            var translationOfUser = await _context.TranslationOfUsers.FindAsync(id);
            if (translationOfUser == null)
            {
                return NotFound();
            }

            _context.TranslationOfUsers.Remove(translationOfUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TranslationOfUserExists(Guid id)
        {
            return _context.TranslationOfUsers.Any(e => e.IdUser == id);
        }
    }
}
