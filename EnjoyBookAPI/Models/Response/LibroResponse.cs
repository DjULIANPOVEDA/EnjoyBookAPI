using System.ComponentModel.DataAnnotations.Schema;

namespace EnjoyBookAPI.Models.Response
{
    public class LibroResponse
    {
        public string Id { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Autor { get; set; } = null!;
        public string Editor { get; set; } = null!;
        public int Npag { get; set; }
        public string Estado { get; set; } = null!;
        public bool EstaVendido { get; set; }
        public bool EstaRentado { get; set; }
        public decimal PrecioVenta { get; set; }
        public decimal PrecioRentaDia { get; set; }
    }
}
