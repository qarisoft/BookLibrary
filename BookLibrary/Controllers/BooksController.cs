using BookLibrary.Data;
using BookLibrary.Entities;
using BookLibrary.Mapping;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController(AppDbContext context) : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> Books()
        {
            var books = await context.Books.
            Include(b => b.Genre)
            .Select(b => b.ToDto())
            .AsNoTracking()
            .ToListAsync();
            
            return Ok(books);
        }
    }
}
