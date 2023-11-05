using AutoMapper;
using EnjoyBookAPI.Controllers;
using EnjoyBookAPI.Mappings;
using EnjoyBookAPI.Models;
using EnjoyBookAPI.Models.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace TestEnjoybook
{
    public class Enjoybook
    {
        public class TestLibroController
        {
            private readonly EnjoyBookDbContext _context;
            private readonly LibroController _controller;
            private LibroRequest testLibro;
            private readonly IMapper _mapper;
            public TestLibroController()
            {
                _context = new EnjoyBookDbContext();
                var configuration = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new MapperProfile());
                });

                _mapper = configuration.CreateMapper();

                _controller = new LibroController(_context, _mapper);
                loginMock();
                testLibro = new LibroRequest()
                {
                    Nombre = Guid.NewGuid()
                  .ToString()
                  .Substring(0, 10),
                    Autor = Guid.NewGuid()
                  .ToString()
                  .Substring(0, 10),
                    Editor = Guid.NewGuid()
                  .ToString()
                  .Substring(0, 10),
                    Npag = new Random().Next(1, 2),
                    Estado = Guid.NewGuid()
                  .ToString()
                  .Substring(0, 10),
                    PrecioRentaDia = new Random().Next(1, 2),
                    PrecioVenta = new Random().Next(1, 2),
                };
            }
            [Fact]
            public async Task TestCrudLibro()
            {
                await TestInsertarLibro();
                await TestActualizarLibro();
                await TestEliminarLibro();
            }
            [Fact]
            public async Task TestGetAllLibros()
            {
                var testCase = await _controller.GetAllLibros();
                Assert.IsType<OkObjectResult>(testCase.Result);
            }
            public async Task TestInsertarLibro()
            {
                //Preparacion
                
                //Prueba
                await _controller.InsertarLibro(testLibro);
                var libro = await _context.Libros.Where(l => l.Nombre.Equals(testLibro.Nombre)).FirstOrDefaultAsync();
                var result = await _context.Libros.FindAsync(libro.Id);
                //Verificacion
                Assert.Equal(testLibro.Nombre, result.Nombre);
            }
            public async Task TestActualizarLibro()
            {
                //Preparacion
                var libro = await _context.Libros.Where(l => l.Nombre.Equals(testLibro.Nombre)).FirstOrDefaultAsync();
                testLibro.Autor = "Author Update";
                //Prueba
                await _controller.ActualizarLibro(testLibro, libro.Id.ToString());
                var libroTest = await _context.Libros.Where(l => l.Nombre.Equals(testLibro.Nombre)).FirstOrDefaultAsync();
                //Verificacion
                var test = await _context.Libros.FindAsync(libro.Id);
                Assert.Equal(testLibro.Autor, libroTest?.Autor);
            }
            public async Task TestEliminarLibro()
            {
                //Preparacion
                var libro = await _context.Libros.Where(l => l.Nombre.Equals(testLibro.Nombre)).FirstOrDefaultAsync();
                //Prueba
                await _controller.Eliminar(libro.Id.ToString());
                //Verificacion
                var test = await _context.Libros.FindAsync(libro.Id.ToString());
                Assert.Equal(test, null);
            }
            private void loginMock()
            {
                var userEntity =_context.Usuarios.Where(u => u.Username.Equals("fischer")).FirstOrDefault();
                var claims = new[]
                {
                    new Claim("UsuarioId", userEntity.Id)
                };
                var identity = new ClaimsIdentity(claims, "TestAuthenticationType");
                var user = new ClaimsPrincipal(identity);
                _controller.ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext { User = user }
                };
            }
        }
    }
}
