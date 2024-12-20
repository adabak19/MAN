using MAN.Shared.Models;
using MAN.Api.Services;
using Microsoft.AspNetCore.Mvc;
using MAN.Shared.Interfaces;

namespace MAN.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookGenreController : ControllerBase
{
    private readonly IBookGenreService _bookGenreService;
    public BookGenreController(IBookGenreService bookGenreService){
        _bookGenreService = bookGenreService;
    }
    [HttpGet]
    public async Task<IActionResult> GetAll(){
        var bookGenres = await _bookGenreService.GetAllAsync();
        return Ok(bookGenres);
    }
    [HttpGet("{id}/{id1}")]
    public async Task<ActionResult<BookGenre>> Get([FromRoute] int id, [FromRoute] int id1){
        var bookGenre = await _bookGenreService.GetAsyncById(id, id1);
        if (bookGenre is null)
            return NotFound();
        return bookGenre;
    }
    [HttpPost]
    public async Task<IActionResult> Create(BookGenre bookGenre){
        await _bookGenreService.Add(bookGenre);
        return CreatedAtAction(nameof(Get), new { id = bookGenre.GenreId, id1 = bookGenre.BookId}, bookGenre);
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
    public async Task<IActionResult> Delete([FromRoute] int id, [FromRoute] int id1){
        var bookGenre = await _bookGenreService.GetAsyncById(id, id1);
        if (bookGenre is null)
            return NotFound();
        await _bookGenreService.Delete(id, id1);
        return NoContent();
    }
}