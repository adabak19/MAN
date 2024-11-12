using MAN.Models;
using MAN.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace MAN.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookReadController : ControllerBase
    {
        public BookReadController()
        {
        }

        [HttpGet]
        public ActionResult<List<BookRead>> GetAll() => BookReadService.GetAll();

        [HttpGet("{profileId}/{bookId}")]
        public ActionResult<BookRead> Get(int profileId, int bookId)
        {
            var bookRead = BookReadService.Get(profileId, bookId);

            if (bookRead == null)
                return NotFound();

            return bookRead;
        }

        [HttpPost]
        public IActionResult Create(BookRead bookRead)
        {
            BookReadService.Add(bookRead);
            return CreatedAtAction(nameof(Get), new { profileId = bookRead.ProfileId, bookId = bookRead.BookId }, bookRead);
        }

        [HttpPut("{profileId}/{bookId}")]
        public IActionResult Update(int profileId, int bookId, BookRead bookRead)
        {
            if (profileId != bookRead.ProfileId || bookId != bookRead.BookId)
                return BadRequest();

            var existingBookRead = BookReadService.Get(profileId, bookId);
            if (existingBookRead is null)
                return NotFound();

            BookReadService.Update(bookRead);

            return NoContent();
        }

        [HttpDelete("{profileId}/{bookId}")]
        public IActionResult Delete(int profileId, int bookId)
        {
            var bookRead = BookReadService.Get(profileId, bookId);

            if (bookRead is null)
                return NotFound();

            BookReadService.Delete(profileId, bookId);

            return NoContent();
        }
    }
}