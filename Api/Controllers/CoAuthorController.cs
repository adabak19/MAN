using MAN.Api.Models;
using MAN.Api.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace MAN.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CoAuthorController : ControllerBase
    {
        private readonly CoAuthorService _coAuthorService;
        public CoAuthorController(CoAuthorService coAuthorService)
        {
            _coAuthorService = coAuthorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(){
            var coAuthor = await _coAuthorService.GetAllAsync();
            return Ok(coAuthor);
        }

        [HttpGet("{bookId}/{authorId}")]
        public async Task<ActionResult<CoAuthors>> Get(int bookId, int authorId)
        {
            var coAuthor = await _coAuthorService.GetAsyncById(bookId, authorId);

            if (coAuthor == null)
                return NotFound();

            return coAuthor;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CoAuthors coAuthor)
        {
            await _coAuthorService.Add(coAuthor);
            return CreatedAtAction(nameof(Get), new { bookId = coAuthor.BookId, authorId = coAuthor.AuthorId }, coAuthor);
        }

        [HttpPut("{bookId}/{authorId}")]
        public async Task<IActionResult> Update(int bookId, int authorId, CoAuthors coAuthor)
        {
            var existingCoAuthor = await _coAuthorService.GetAsyncById(bookId, authorId);
            if (existingCoAuthor is null)
                return NotFound();

            await _coAuthorService.Update(coAuthor);
            return NoContent();
        }

        [HttpDelete("{bookId}/{authorId}")]
        public async Task<IActionResult> Delete(int bookId, int authorId)
        {
            var coAuthor = await _coAuthorService.GetAsyncById(bookId, authorId);

            if (coAuthor is null)
                return NotFound();

            await _coAuthorService.Delete(bookId, authorId);

            return NoContent();
        }
    }
}