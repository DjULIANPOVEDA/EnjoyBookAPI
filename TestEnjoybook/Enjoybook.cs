using AutoMapper;
using EnjoyBookAPI.Controllers;
using EnjoyBookAPI.Models;
using EnjoyBookAPI.Models.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
                _controller = new LibroController(_context, _mapper);
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
            public async Task testCrudLibro()
            {
                await TestGetAllLibros();
                await TestActualizarLibro();
                await TestInsertarLibro(); 
            }
            [Fact]
                public async Task TestGetAllLibros()
                {
                    var testCase = await _controller.GetAllLibros();
                    Assert.IsType<OkObjectResult>(testCase);
                }
            public async Task TestInsertarLibro()
            {
                //Preparacion
                
                //Prueba
                await _controller.InsertarLibro(testLibro);
                var libro = await _context.Libros.Where(l => l.Nombre.Equals(testLibro.Nombre)).FirstOrDefaultAsync();
                var result = await _context.Libros.FindAsync(libro.Id);
                //Verificacion
                var test1=_mapper.Map<LibroRequest>(result);
                Assert.Equal(testLibro, test1);
            }
            public async Task TestActualizarLibro()
            {
                //Preparacion
                var libro = await _context.Libros.Where(l => l.Nombre.Equals(testLibro.Nombre)).FirstOrDefaultAsync();
                testLibro.Autor = "Author Update";
                //Prueba
                await _controller.ActualizarLibro(testLibro, libro.Id.ToString()); 
                //Verificacion
                var test = await _context.Libros.FindAsync(libro.Id);
                Assert.Equal(testLibro.Autor, libro?.Autor);
            }
            public async Task TestEliminar()
            {
                //Preparacion
                var libro = await _context.Libros.Where(l => l.Nombre.Equals(testLibro.Nombre)).FirstOrDefaultAsync();
                //Prueba
                await _controller.Eliminar(libro.Id.ToString());
                //Verificacion
                var test = await _context.Libros.FindAsync(libro.Id.ToString());
                Assert.IsType<NotFoundResult>(test);
            }
        }
    }
    }
