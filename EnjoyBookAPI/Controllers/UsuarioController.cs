using AutoMapper;
using EnjoyBookAPI.Models;
using EnjoyBookAPI.Models.Request;
using EnjoyBookAPI.Models.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EnjoyBookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly EnjoyBookDbContext _context;
        private readonly IMapper _mapper;
        public UsuarioController(EnjoyBookDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UsuarioResponse>> Login([FromBody] LoginRequest request)
        {
            var user = await _context.Usuarios.Where(x => x.Username.Equals(request.UserName)).FirstOrDefaultAsync();
            if (user == null) return BadRequest();
            if (!VerifyPassword(request.Password, user.Password)) return BadRequest();
            var userResponse = _mapper.Map<UsuarioResponse>(user);
            userResponse.Token = GenereteToken(user);
            return Ok(userResponse);
        }

        [HttpPost("Register")]
        public async Task<ActionResult<bool>> Register([FromBody] RegisterRequest request)
        {
            var user = _mapper.Map<Usuario>(request);
            user.Rol = Roles.User.ToString();
            user.Id = Guid.NewGuid().ToString();
            user.Password = EncryptPassword(request.Password);

            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
            return Ok(true);
        }


        private string GenereteToken(Usuario user)
        {
            var claims = new[]
            {
                new Claim("UsuarioId", user.Id)
            };

            var symetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("1e98df0c-6999-40ce-9ab2-e6586e7e2f6a"));
            var signingCredentials = new SigningCredentials(symetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var jwtSecurityToken = new JwtSecurityToken(
            claims: claims,
                expires: DateTime.UtcNow.AddMinutes(120),
                signingCredentials: signingCredentials
                );

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }


        private string EncryptPassword(string password)
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt(10);

            string hash = BCrypt.Net.BCrypt.HashPassword(password, salt);

            return hash;
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
