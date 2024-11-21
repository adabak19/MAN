using MAN.Models;
using MAN.Services;
using Microsoft.AspNetCore.Mvc;

namespace MAN.Controllers;

[ApiController]
[Route("[controller]")]
public class BookController : ControllerBase
{
    private readonly BookService _bookService;
    public BookController(BookService bookService){
        _bookService = bookService;
    }
    [HttpGet]
    public async Task<IActionResult> GetAll(){
         var books = await _bookService.GetAllAsync();
         return Ok(books);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<Book>> Get(int id){
        var book = await _bookService.GetAsyncById(id);
        if (book is null)
            return NotFound();
        return book;
    }
    [HttpPost]
    public async Task<IActionResult> Create(Book book){
        await _bookService.Add(book);
        return CreatedAtAction(nameof(Get), new { id = book.Id}, book);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Book book){
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
}