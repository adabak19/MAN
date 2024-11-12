using MAN.Models;
using MAN.Services;
using Microsoft.AspNetCore.Mvc;

namespace MAN.Controllers;

[ApiController]
[Route("[controller]")]
public class GenreController : ControllerBase
{
    public GenreController(){

    }
    [HttpGet]
    public ActionResult<List<Genre>> GetAll() => GenreService.GetAll();
    [HttpGet("{id}")]
    public ActionResult<Genre> Get(int id){
        var genre = GenreService.Get(id);
        if (genre is null)
            return NotFound();
        return genre;
    }
    [HttpPost]
    public IActionResult Create(Genre genre){
        GenreService.Add(genre);
        return CreatedAtAction(nameof(Get), new { id = genre.Id}, genre);
    }
    [HttpPut("{id}")]
    public IActionResult Update(int id, Genre genre){
        var existingGenre = GenreService.Get(id);
        if(existingGenre is null)
            return NotFound();
        GenreService.Update(genre);
        return NoContent();
    }
    [HttpDelete("{id}")]
    public IActionResult Delete(int id){
        var genre = GenreService.Get(id);
        if (genre is null)
            return NotFound();
        GenreService.Delete(id);
        return NoContent();
    }
}