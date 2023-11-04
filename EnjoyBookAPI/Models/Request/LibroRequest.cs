using System.ComponentModel.DataAnnotations.Schema;

namespace EnjoyBookAPI.Models.Request
{
    public class LibroRequest
    {
        public string Nombre { get; set; } = null!;
        public string Autor { get; set; } = null!;
        public string Editor { get; set; } = null!;
        public int Npag { get; set; }
        public string Estado { get; set; } = null!;
        public decimal PrecioVenta { get; set; }
        public decimal PrecioRentaDia { get; set; }
    }
}
