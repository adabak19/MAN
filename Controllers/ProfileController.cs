using MAN.Models;
using MAN.Services;
using Microsoft.AspNetCore.Mvc;

namespace MAN.Controllers;

[ApiController]
[Route("[controller]")]
public class ProfileController : ControllerBase
{
    private readonly ProfileService _profileService;
    public ProfileController(ProfileService profileService){
        _profileService = profileService;
    }
    [HttpGet]
    public async Task<IActionResult> GetAll(){
        var profile = await _profileService.GetAllAsync();
        return Ok(profile);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<Profile>> Get(int id){
        var profile = await _profileService.GetAsyncById(id);
        if (profile is null)
            return NotFound();
        return profile;
    }
    [HttpPost]
    public async Task<IActionResult> Create(Profile profile){
        await _profileService.AddAsync(profile);
        return CreatedAtAction(nameof(Get), new { id = profile.Id}, profile);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Profile profile){
        var existingProfile = await _profileService.GetAsyncById(id);
        if(existingProfile is null)
            return NotFound();
        await _profileService.UpdateAsync(profile);
        return NoContent();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id){
        var profile = _profileService.GetAsyncById(id);
        if (profile is null)
            return NotFound();
        await _profileService.DeleteAsync(id);
        return NoContent();
    }
}