using EnjoyBookAPI.Models;
using EnjoyBookAPI.Models.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnjoyBookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly EnjoyBookDbContext _context;

        public UsuarioController(EnjoyBookDbContext context)
        {
            _context = context;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<Usuario?>> Login([FromBody] LoginRequest request)
        {
            return Ok(await _context.Usuarios.FirstOrDefaultAsync());
        }

        [HttpPost("Register")]
        public async Task<ActionResult<Usuario?>> Register([FromBody] RegisterRequest request)
        {
            return Ok(await _context.Usuarios.FirstOrDefaultAsync());
        }
    }
}
