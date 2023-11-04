using AutoMapper;
using EnjoyBookAPI.Models;
using EnjoyBookAPI.Models.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EnjoyBookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibroController : ControllerBase
    {
        private readonly EnjoyBookDbContext _context;
        private readonly IMapper _mapper;
        public LibroController(EnjoyBookDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<List<Libro>>> GetAllLibros()
        {
            return Ok(await _context.Libros.ToListAsync());
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<bool>> InsertarLibro([FromBody] LibroRequest request)
        {
            var userId = User.FindFirst("UsuarioId")?.Value;
            var libro = _mapper.Map<Libro>(request);
            libro.Id = Guid.NewGuid().ToString();
            libro.UsuarioId = userId;
            libro.EstaVendido = false;
            libro.EstaRentado = false;

            return Ok(true);
        }
    }
}
