using MAN.Shared.Models;
using MAN.Api.Services;
using Microsoft.AspNetCore.Mvc;
using MAN.Shared.Interfaces;

namespace MAN.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GenreController : ControllerBase
{
    private readonly IGenreService _genreService;
    public GenreController(IGenreService genreService){
        _genreService = genreService;
    }
    [HttpGet]
    public async Task<IActionResult> GetAll(){
        var genre = await _genreService.GetAllAsync();
        return Ok(genre);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<Genre>> Get(int id){
        var genre = await _genreService.GetAsyncById(id);
        if (genre is null)
            return NotFound();
        return genre;
    }
    [HttpPost]
    public async Task<IActionResult> Create(Genre genre){
        await _genreService.Add(genre);
        return CreatedAtAction(nameof(Get), new { id = genre.Id}, genre);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Genre genre){
        var existingGenre = await _genreService.GetAsyncById(id);
        if(existingGenre is null)
            return NotFound();
        await _genreService.Update(genre);
        return NoContent();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id){
        var genre = _genreService.GetAsyncById(id);
        if (genre is null)
            return NotFound();
        await _genreService.Delete(id);
        return NoContent();
    }
}