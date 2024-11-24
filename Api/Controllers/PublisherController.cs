using LibraryManagement.Shared.Models;
using MAN.Api.Services;
using Microsoft.AspNetCore.Mvc;
using LibraryManagement.Shared.Interfaces;


namespace MAN.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PublisherController : ControllerBase
    {
        private readonly IPublisherService _publisherService;
    public PublisherController(IPublisherService publisherService){
        _publisherService = publisherService;
    }
    [HttpGet]
    public async Task<IActionResult> GetAll(){
        var publisher = await _publisherService.GetAllAsync();
        return Ok(publisher);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<Publisher>> Get(int id){
        var publisher = await _publisherService.GetAsyncById(id);
        if (publisher is null)
            return NotFound();
        return publisher;
    }
    [HttpPost]
    public async Task<IActionResult> Create(Publisher publisher){
        await _publisherService.AddAsync(publisher);
        return CreatedAtAction(nameof(Get), new { id = publisher.Id}, publisher);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Publisher publisher){
        var existingPublisher = await _publisherService.GetAsyncById(id);
        if(existingPublisher is null)
            return NotFound();
        await _publisherService.UpdateAsync(publisher);
        return NoContent();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id){
        var Publisher = _publisherService.GetAsyncById(id);
        if (Publisher is null)
            return NotFound();
        await _publisherService.DeleteAsync(id);
        return NoContent();
    }
    }
}
