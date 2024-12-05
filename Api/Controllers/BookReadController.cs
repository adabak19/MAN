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

        // Get all book reads
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var bookReads = await _bookReadService.GetAllAsync();
            if (!bookReads.Any())
                return NotFound("No book reads found.");
            return Ok(bookReads);
        }

        // Get book read by profileId and bookId
        [HttpGet("{profileId}/{bookId}")]
        public async Task<ActionResult<BookRead>> GetByProfileAndBookId(int profileId, int bookId)
        {
            var bookRead = await _bookReadService.GetAsyncById(profileId, bookId);
            if (bookRead == null)
                return NotFound($"No book read found for ProfileId: {profileId} and BookId: {bookId}");
            return Ok(bookRead);
        }

        // Get all book reads for a specific book by bookId
        [HttpGet("book/{bookId}")]
        public async Task<IActionResult> GetByBookId(int bookId)
        {
            var bookReads = await _bookReadService.GetAsyncByBookId(bookId);
            if (!bookReads.Any())
                return NotFound($"No book reads found for BookId: {bookId}");
            return Ok(bookReads);
        }

        // Get all book reads for a specific profile by profileId
        [HttpGet("profile/{profileId}")]
        public async Task<IActionResult> GetByProfileId(int profileId)
        {
            var bookReads = await _bookReadService.GetAsyncByProfileId(profileId);
            if (!bookReads.Any())
                return NotFound($"No book reads found for ProfileId: {profileId}");
            return Ok(bookReads);
        }

        // Create a new book read
        [HttpPost]
        public async Task<IActionResult> Create(BookRead bookRead)
        {
            var createdBookRead = await _bookReadService.Add(bookRead);
            return CreatedAtAction(nameof(GetByProfileAndBookId), new { profileId = bookRead.ProfileId, bookId = bookRead.BookId }, createdBookRead);
        }

        // Update a book read
        [HttpPut("{profileId}/{bookId}")]
        public async Task<IActionResult> Update(int profileId, int bookId, BookRead bookRead)
        {
            var existingBookRead = await _bookReadService.GetAsyncById(profileId, bookId);
            if (existingBookRead is null)
                return NotFound($"No book read found for ProfileId: {profileId} and BookId: {bookId}");

            await _bookReadService.Update(bookRead);
            return NoContent();
        }

        // Delete a book read
        [HttpDelete("{profileId}/{bookId}")]
        public async Task<IActionResult> Delete(int profileId, int bookId)
        {
            var bookRead = await _bookReadService.GetAsyncById(profileId, bookId);
            if (bookRead is null)
                return NotFound($"No book read found for ProfileId: {profileId} and BookId: {bookId}");

            await _bookReadService.Delete(profileId, bookId);
            return NoContent();
        }
        [HttpGet("reading")]
        public async Task<IActionResult> GetAllReading(){
            var bookReads = await _bookReadService.GetAllReading();
            return Ok(bookReads);
        }
    }
}
