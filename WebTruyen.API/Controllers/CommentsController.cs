using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebTruyen.Library.Data;
using WebTruyen.Library.Entities.Request;
using WebTruyen.Library.Entities.ApiModel;
using WebTruyen.API.Repository.CommentDI;
using System.Security.Claims;

namespace WebTruyen.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _comment;

        public CommentsController(ICommentService context)
        {
            _comment = context;
        }

        // GET: api/Comments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommentAM>>> GetComments()
        {
            return Ok(await _comment.GetComments());
        }

        // GET: api/Comments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CommentAM>> GetComment(Guid id)
        {
            var comment = await _comment.GetComment(id);

            if (comment == null) {
                return NotFound();
            }

            return comment;
        }


        // GET: api/Comments/GetCommentInComic?idComic=&skip=&take=
        [HttpGet("GetCommentInComic")]
        public async Task<ActionResult<List<CommentAM>>> GetCommentInComic(Guid idComic, int skip = 0, int take = 10)
        {
            var comment = await _comment.GetCommentInComic(idComic, skip, take);

            if (comment == null) {
                return NotFound();
            }

            return Ok(comment);
        }
        // GET: api/Comments/GetCommentInComic?idChapter=&skip=&take=
        [HttpGet("GetCommentInChapter")]
        public async Task<ActionResult<List<CommentAM>>> GetCommentInChapter(Guid idChapter, int skip = 0, int take = 10)
        {
            var comment = await _comment.GetCommentInChapter(idChapter, skip, take);

            if (comment == null) {
                return NotFound();
            }

            return Ok(comment);
        }

        // GET: api/Comments/GetCommentChildInComic?idComic=&idCommentReply=&skip=&take=
        [HttpGet("GetCommentChildInComic")]
        public async Task<ActionResult<List<CommentAM>>> GetCommentChildInComic(Guid idComic, Guid idCommentReply, int skip = 0, int take = 10)
        {
            var comment = await _comment.GetCommentChildInComic(idComic, idCommentReply, skip, take);

            if (comment == null) {
                return NotFound();
            }

            return comment;
        }

        // GET: api/Comments/GetCommentChildInChapter?idChapter=&idCommentReply=&skip=&take=
        [HttpGet("GetCommentChildInChapter")]
        public async Task<ActionResult<List<CommentAM>>> GetCommentChildInChapter(Guid idChapter, Guid idCommentReply, int skip = 0, int take = 10)
        {
            var comment = await _comment.GetCommentChildInChapter(idChapter, idCommentReply, skip, take);

            if (comment == null) {
                return NotFound();
            }

            return comment;
        }

        // PUT: api/Comments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutComment(Guid id, CommentAM comment)
        {
            if (id != comment.Id) {
                return BadRequest();
            }

            var result = await _comment.PutComment(id, comment);

            if (!result.isSuccess)
                return NotFound(result.messages);

            return NoContent();
        }

        // POST: api/Comments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<CommentAM>> PostComment(CommentRequest comment)
        {
            if (comment.IdComic == Guid.Empty && comment.IdChapter == Guid.Empty)
                return BadRequest("Id Comic không được null");
            if (comment.IdCommentReply == null && comment.Level > 0)
                return BadRequest("Level lớn hơn 1 thì IdCommentReply không dược null");
            var result = await _comment.PostComment(comment);
            if (!result.isSuccess)
                return BadRequest(result.messages);

            return CreatedAtAction("GetComment", new { id = comment.Id }, comment);
        }

        // DELETE: api/Comments/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteComment(Guid id)
        {
            var userID = User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.NameIdentifier)?.Value;
            var userRole = User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.Role)?.Value;

            var result = await _comment.DeleteComment(id, Guid.Parse(userID), userRole);
            if (!result) {
                return NoContent();
            }

            return Ok();
        }

    }
}
