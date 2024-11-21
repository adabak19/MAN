using MAN.Models;
using MAN.Services;
using Microsoft.AspNetCore.Mvc;

namespace MAN.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthorController : ControllerBase
{
    private readonly AuthorService _authorService;
    public AuthorController(AuthorService authorService){
        _authorService = authorService;
    }
    [HttpGet]
    public async Task<IActionResult> GetAll(){
        var authors = await _authorService.GetAllAsync();
        return Ok(authors);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<Author>> Get(int id){
        var author = await _authorService.GetAsyncById(id);
        if (author is null)
            return NotFound();
        return author;
    }
    [HttpPost]
    public async Task<IActionResult> Create(Author author){
        await _authorService.Add(author);
        return CreatedAtAction(nameof(Get), new { id = author.Id}, author);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Author author){
        var existingAuthor = await _authorService.GetAsyncById(id);
        if(existingAuthor is null)
            return NotFound();
        await _authorService.Update(author);
        return NoContent();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id){
        var author = _authorService.GetAsyncById(id);
        if (author is null)
            return NotFound();
        await _authorService.Delete(id);
        return NoContent();
    }
}
