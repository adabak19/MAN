using MAN.Models;
using MAN.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace MAN.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CoAuthorController : ControllerBase
    {
        public CoAuthorController()
        {
        }

        [HttpGet]
        public ActionResult<List<CoAuthors>> GetAll() => CoAuthorService.GetAll();

        [HttpGet("{bookId}/{authorId}")]
        public ActionResult<CoAuthors> Get(int bookId, int authorId)
        {
            var coAuthor = CoAuthorService.Get(bookId, authorId);

            if (coAuthor == null)
                return NotFound();

            return coAuthor;
        }

        [HttpPost]
        public IActionResult Create(CoAuthors coAuthor)
        {
            CoAuthorService.Add(coAuthor);
            return CreatedAtAction(nameof(Get), new { bookId = coAuthor.BookId, authorId = coAuthor.AuthorId }, coAuthor);
        }

        [HttpPut("{bookId}/{authorId}")]
        public IActionResult Update(int bookId, int authorId, CoAuthors coAuthor)
        {
            if (bookId != coAuthor.BookId || authorId != coAuthor.AuthorId)
                return BadRequest();

            var existingCoAuthor = CoAuthorService.Get(bookId, authorId);
            if (existingCoAuthor is null)
                return NotFound();

            CoAuthorService.Update(coAuthor);

            return NoContent();
        }

        [HttpDelete("{bookId}/{authorId}")]
        public IActionResult Delete(int bookId, int authorId)
        {
            var coAuthor = CoAuthorService.Get(bookId, authorId);

            if (coAuthor is null)
                return NotFound();

            CoAuthorService.Delete(bookId, authorId);

            return NoContent();
        }
    }
}