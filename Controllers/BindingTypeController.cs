using MAN.Models;
using MAN.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace MAN.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BindingTypeController : ControllerBase
    {
        public BindingTypeController()
        {
        }

        [HttpGet]
        public ActionResult<List<BindingType>> GetAll() => BindingTypeService.GetAll();

        [HttpGet("{id}")]
        public ActionResult<BindingType> Get(int id)
        {
            var bindingType = BindingTypeService.Get(id);
            if (bindingType is null)
                return NotFound();
            return bindingType;
        }

        [HttpPost]
        public IActionResult Create(BindingType bindingType)
        {
            BindingTypeService.Add(bindingType);
            return CreatedAtAction(nameof(Get), new { id = bindingType.Id }, bindingType);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, BindingType bindingType)
        {
            if (id != bindingType.Id)
                return BadRequest("BindingType ID mismatch");

            var existingBindingType = BindingTypeService.Get(id);
            if (existingBindingType is null)
                return NotFound();

            BindingTypeService.Update(bindingType);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var bindingType = BindingTypeService.Get(id);
            if (bindingType is null)
                return NotFound();
            BindingTypeService.Delete(id);
            return NoContent();
        }
    }
}
