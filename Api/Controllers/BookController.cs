using MAN.Shared.Models;
using MAN.Shared.DTO;
using MAN.Api.Services;
using Microsoft.AspNetCore.Mvc;
using MAN.Shared.Interfaces;

namespace MAN.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookController : ControllerBase
{
    private readonly IBookService _bookService;
    public BookController(IBookService bookService){
        _bookService = bookService;
    }
    [HttpGet]
    public async Task<IActionResult> GetAll(){
         var books = await _bookService.GetAllAsync();
         return Ok(books);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<BookDto>> Get(int id){
        var book = await _bookService.GetAsyncById(id);
        if (book is null)
            return NotFound();
        return Ok(book);
    }
    [HttpPost]
    public async Task<IActionResult> Create(Book book){
        await _bookService.Add(book);
        return CreatedAtAction(nameof(Get), new { id = book.Id}, book);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, BookDto book){
        var existingBook = await _bookService.GetAsyncById(id);
        if(existingBook is null)
            return NotFound();
        await _bookService.Update(book);
        return NoContent();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id){
        var book = await _bookService.GetAsyncById(id);
        if (book is null)
            return NotFound();
        await _bookService.Delete(id);
        return NoContent();
    }

[HttpGet("book/{profileId}")]
public async Task<ActionResult<List<BookDto>>> SearchBooksForUser(int profileId, [FromQuery] string? title, [FromQuery] string? author, [FromQuery] string? genre)
{
    var books = await _bookService.SearchBooksForUserAsync(profileId, title, author, genre);
    return Ok(books);
}

 // Search books by title, author, or genre
    [HttpGet("search")]
    public async Task<IActionResult> Search(string? title, string? author, string? genre)
    {
        var books = await _bookService.SearchBooksAsync(title, author, genre);
        return Ok(books);
    }
}