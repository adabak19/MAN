using MAN.Shared.Models;
using MAN.Api.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using MAN.Shared.Interfaces;

namespace MAN.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookReadController : ControllerBase
    {
        private readonly IBookReadService _bookReadService;
        public BookReadController(IBookReadService bookReadService)
        {
            _bookReadService = bookReadService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(){
            var bookReads = await _bookReadService.GetAllAsync();
            return Ok(bookReads);
        }

        [HttpGet("{profileId}/{bookId}")]
        public async Task<ActionResult<BookRead>> Get(int profileId, int bookId)
        {
            var bookRead = await _bookReadService.GetAsyncById(profileId, bookId);
            if (bookRead == null)
                return NotFound();
            return bookRead;
        }

        [HttpGet("{bookId}")]
        public async Task<IActionResult> GetAsyncByBookId(int bookId){
            var bookReads = await _bookReadService.GetAsyncByBookId(bookId);
            return Ok(bookReads);
        }

        [HttpPost]
        public async Task<IActionResult> Create(BookRead bookRead)
        {
            await _bookReadService.Add(bookRead);
            return CreatedAtAction(nameof(Get), new { profileId = bookRead.ProfileId, bookId = bookRead.BookId }, bookRead);
        }

        [HttpPut("{profileId}/{bookId}")]
        public async Task<IActionResult> Update(int profileId, int bookId, BookRead bookRead)
        {
            var existingBookRead = await _bookReadService.GetAsyncById(profileId, bookId);
            if (existingBookRead is null)
                return NotFound();

            await _bookReadService.Update(bookRead);

            return NoContent();
        }

        [HttpDelete("{profileId}/{bookId}")]
        public async Task<IActionResult> Delete(int profileId, int bookId)
        {
            var bookRead = await _bookReadService.GetAsyncById(profileId, bookId);

            if (bookRead is null)
                return NotFound();

            await _bookReadService.Delete(profileId, bookId);

            return NoContent();
        }
    }
}