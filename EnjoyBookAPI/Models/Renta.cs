using System.ComponentModel.DataAnnotations.Schema;

namespace EnjoyBookAPI.Models
{
    public class Renta
    {
        [Column("Id")]
        public string Id { get; set; } = null!;
        [Column("IdLibro")]
        public string IdLibro { get; set; } = null!;
        [Column("IdUsuario")]
        public string IdUsuario { get; set; } = null!;
        [Column("FechaRenta")]
        public DateTime FechaRenta { get; set; }
        [Column("DiasRenta")]
        public int DiasRenta { get; set; }
    }
}
