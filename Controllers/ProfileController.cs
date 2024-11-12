using MAN.Models;
using MAN.Services;
using Microsoft.AspNetCore.Mvc;

namespace MAN.Controllers;

[ApiController]
[Route("[controller]")]
public class ProfileController : ControllerBase
{
    public ProfileController(){

    }
    [HttpGet]
    public ActionResult<List<Profile>> GetAll() => ProfileService.GetAll();
    [HttpGet("{id}")]
    public ActionResult<Profile> Get(int id){
        var profile = ProfileService.Get(id);
        if (profile is null)
            return NotFound();
        return profile;
    }
    [HttpPost]
    public IActionResult Create(Profile profile){
        ProfileService.Add(profile);
        return CreatedAtAction(nameof(Get), new { id = profile.Id}, profile);
    }
    [HttpPut("{id}")]
    public IActionResult Update(int id, Profile profile){
        var existingProfile = ProfileService.Get(id);
        if(existingProfile is null)
            return NotFound();
        ProfileService.Update(profile);
        return NoContent();
    }
    [HttpDelete("{id}")]
    public IActionResult Delete(int id){
        var profile = ProfileService.Get(id);
        if (profile is null)
            return NotFound();
        ProfileService.Delete(id);
        return NoContent();
    }
}