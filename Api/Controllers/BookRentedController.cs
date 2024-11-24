using MAN.Shared.Models;
using MAN.Api.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using MAN.Shared.Interfaces;

namespace MAN.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookRentedController : ControllerBase
    {
        private readonly IBookRentedService _bookRentedService;
        public BookRentedController(IBookRentedService bookRentedService)
        {
            _bookRentedService = bookRentedService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(){
            var booksRented = await _bookRentedService.GetAllAsync();
            return Ok(booksRented);
        }

        [HttpGet("{profileId}/{bookId}")]
        public async Task<ActionResult<BookRented>> Get(int profileId, int bookId)
        {
            var bookRented = await _bookRentedService.GetAsyncById(profileId, bookId);
            if (bookRented == null)
                return NotFound();
            return bookRented;
        }

        [HttpPost]
        public async Task<IActionResult> Create(BookRented bookRented)
        {
            await _bookRentedService.Add(bookRented);
            return CreatedAtAction(nameof(Get), new { profileId = bookRented.ProfileId, bookId = bookRented.BookId }, bookRented);
        }

        [HttpPut("{profileId}/{bookId}")]
        public async Task<IActionResult> Update(int profileId, int bookId, BookRented BookRented)
        {
            var existingBookRented = await _bookRentedService.GetAsyncById(profileId, bookId);
            if (existingBookRented is null)
                return NotFound();

            await _bookRentedService.Update(BookRented);

            return NoContent();
        }

        [HttpDelete("{profileId}/{bookId}")]
        public async Task<IActionResult> Delete(int profileId, int bookId)
        {
            var bookRented = await _bookRentedService.GetAsyncById(profileId, bookId);

            if (bookRented is null)
                return NotFound();

            await _bookRentedService.Delete(profileId, bookId);

            return NoContent();
        }
    }
}