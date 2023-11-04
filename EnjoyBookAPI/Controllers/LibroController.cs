using EnjoyBookAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnjoyBookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibroController : ControllerBase
    {
        private readonly EnjoyBookDbContext _context;
        public LibroController(EnjoyBookDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<List<Libro>>> getAllLibros()
        {
            return Ok(await _context.Libros.ToListAsync());
        }
    }
}
