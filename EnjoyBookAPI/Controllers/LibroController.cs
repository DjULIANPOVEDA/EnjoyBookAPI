﻿using AutoMapper;
using EnjoyBookAPI.Models;
using EnjoyBookAPI.Models.Request;
using EnjoyBookAPI.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
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
        public async Task<ActionResult<List<LibroResponse>>> GetAllLibrosFiltered()
        {
            var libros = await _context.Libros.Where(l=>!l.EstaVendido && !l.EstaRentado).ToListAsync();
            var libroResult = _mapper.Map<List<LibroResponse>>(libros);
            return Ok(libroResult);
        }

        [HttpGet("all")]
        [Authorize]
        public async Task<ActionResult<List<LibroWithUserResponse>>> GetAllLibros()
        {
            var libros = await _context.Libros.Include(l => l.Usuario).ToListAsync();

            var libroResult = _mapper.Map<List<LibroWithUserResponse>>(libros);
            return Ok(libroResult);
        }

        [HttpGet("Usuario")]
        [Authorize]
        public async Task<ActionResult<List<LibroResponse>>> GetAllLibrosByUser()
        {
            var userId = User.FindFirst("UsuarioId")?.Value;
            var libros = await _context.Libros.Where(l=>l.UsuarioId.Equals(userId)).ToListAsync();
            var libroResult = _mapper.Map<List<LibroResponse>>(libros);
            return Ok(libroResult);
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
            await _context.Libros.AddAsync(libro);
            await _context.SaveChangesAsync();

            return Ok(true);
        }

        [HttpPut("{libroId}")]
        [Authorize]
        public async Task<ActionResult<bool>> ActualizarLibro([FromBody] LibroRequest request, string libroId)
        {
            var libro = await _context.Libros.Where(l => l.Id.Equals(libroId)).FirstOrDefaultAsync();
            libro.Nombre = request.Nombre;
            libro.Autor = request.Autor;
            libro.Editor = request.Editor;
            libro.Npag = request.Npag;
            libro.Estado = request.Estado;
            libro.PrecioVenta = request.PrecioVenta;
            libro.PrecioRentaDia = request.PrecioRentaDia;

            _context.Entry(libro).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return Ok(true);
        }

        [HttpDelete("Eliminar/{libroId}")]
        [Authorize]
        public async Task<ActionResult<bool>> Eliminar(string libroId)
        {
            var EliminarLibro = await _context.Libros.FindAsync(libroId);
            _context.Libros.Remove(EliminarLibro);
            await _context.SaveChangesAsync();
            return Ok(true);
        }
        [HttpPost("Alquilar")]
        [Authorize]
        public async Task<ActionResult<bool>> Alquilar(RentaRequest request)
        {
            var userId = User.FindFirst("UsuarioId")?.Value;
            var renta = _mapper.Map<Renta>(request);
            var libro = await _context.Libros.Where(l => l.Id.Equals(request.LibroId)).FirstOrDefaultAsync();
            if (libro.EstaRentado || libro.EstaVendido) return Ok(false);
            renta.Id = Guid.NewGuid().ToString();
            renta.FechaRenta = DateTime.Now.ToString("u");
            renta.UsuarioId = userId;
            await _context.Rentas.AddAsync(renta);
            libro.EstaRentado = true;
            libro.AlquiladorId = userId;
            _context.Entry(libro).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(true);
        }
        [HttpPost("Comprar/{libroId}")]
        [Authorize]
        public async Task<ActionResult<bool>> ComprarLibro(string libroId)
        {
            var userId = User.FindFirst("UsuarioId")?.Value;
            var libro = await _context.Libros.Where(l => l.Id.Equals(libroId)).FirstOrDefaultAsync();
            if(libro.EstaRentado || libro.EstaVendido) return Ok(false);
            libro.CompradorId = userId;
            libro.FechaVenta = DateTime.Now.ToString("u");
            _context.Entry(libro).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(true);
        }
    }
}
// ALQUILAR 
// VENDER
// post 
