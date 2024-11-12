using MAN.Models;
using MAN.Services;
using Microsoft.AspNetCore.Mvc;


namespace MAN.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PublisherController : ControllerBase
    {
        public PublisherController()
        {
        }

        [HttpGet]
        public ActionResult<List<Publisher>> GetAll() => PublisherService.GetAll();

        [HttpGet("{id}")]
        public ActionResult<Publisher> Get(int id)
        {
            var publisher = PublisherService.Get(id);
            if (publisher is null)
                return NotFound();
            return publisher;
        }

        [HttpPost]
        public IActionResult Create(Publisher publisher)
        {
            PublisherService.Add(publisher);
            return CreatedAtAction(nameof(Get), new { id = publisher.Id }, publisher);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Publisher publisher)
        {
            var existingPublisher = PublisherService.Get(id);
            if (existingPublisher is null)
                return NotFound();
            PublisherService.Update(publisher);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var publisher = PublisherService.Get(id);
            if (publisher is null)
                return NotFound();
            PublisherService.Delete(id);
            return NoContent();
        }
    }
}
