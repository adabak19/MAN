using MAN.Models;
using MAN.Services;
using Microsoft.AspNetCore.Mvc;

namespace MAN.Controllers;

[ApiController]
[Route("[controller]")]
public class BookGenreController : ControllerBase
{
    private readonly BookGenreService _bookGenreService;
    public BookGenreController(BookGenreService bookGenreService){
        _bookGenreService = bookGenreService;
    }
    [HttpGet]
    public async Task<IActionResult> GetAll(){
        var bookGenres = await _bookGenreService.GetAllAsync();
        return Ok(bookGenres);
    }
    [HttpGet("{id}/{id1}")]
    public async Task<ActionResult<BookGenre>> Get(int id, int id1){
        var bookGenre = await _bookGenreService.GetAsyncById(id, id1);
        if (bookGenre is null)
            return NotFound();
        return bookGenre;
    }
    [HttpPost]
    public async Task<IActionResult> Create(BookGenre bookGenre){
        await _bookGenreService.Add(bookGenre);
        return CreatedAtAction(nameof(Get), new { id = bookGenre.BookId}, bookGenre);
    }
    [HttpPut("{id}/{id1}")]
    public async Task<IActionResult> Update(int id, int id1, BookGenre bookGenre){
        var existingBookGenre = await _bookGenreService.GetAsyncById(id, id1);
        if(existingBookGenre is null)
            return NotFound();
        await _bookGenreService.Update(bookGenre);
        return NoContent();
    }
    [HttpDelete("{id}/{id1}")]
    public async Task<IActionResult> Delete(int id, int id1){
        var bookGenre = await _bookGenreService.GetAsyncById(id, id1);
        if (bookGenre is null)
            return NotFound();
        await _bookGenreService.Delete(id, id1);
        return NoContent();
    }
}