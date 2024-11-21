using MAN.Api.Models;
using MAN.Api.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace MAN.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BindingTypeController : ControllerBase
    {
        private readonly BindingTypeService _bindingTypeService;

        public BindingTypeController(BindingTypeService bindingTypeService){
            _bindingTypeService = bindingTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(){
            var bindingTypes = await _bindingTypeService.GetAllAsync();
            return Ok(bindingTypes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BindingType>> Get(int id)
        {
            var bindingType = await _bindingTypeService.GetAsyncById(id);
            if (bindingType is null)
                return NotFound();
            return bindingType;
        }

        [HttpPost]
        public async Task<IActionResult> Create(BindingType bindingType)
        {
            await _bindingTypeService.Add(bindingType);
            return CreatedAtAction(nameof(Get), new { id = bindingType.Id }, bindingType);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, BindingType bindingType)
        {
            var existingBindingType = await _bindingTypeService.GetAsyncById(id);
            if (existingBindingType is null)
                return NotFound();
            await _bindingTypeService.Update(bindingType);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var bindingType = await _bindingTypeService.GetAsyncById(id);
            if (bindingType is null)
                return NotFound();
            await _bindingTypeService.Delete(id);
            return NoContent();
        }
    }
}