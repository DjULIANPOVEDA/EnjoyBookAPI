using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnjoyBookAPI.Models
{
    [Table("renta")]
    public class Renta
    {
        [Key, Column("Id")]
        public string Id { get; set; } = null!;
        [Column("IdLibro")]
        public string LibroId { get; set; } = null!;
        [Column("IdUsuario")]
        public string UsuarioId { get; set; } = null!;
        [Column("FechaRenta")]
        public DateTime FechaRenta { get; set; }
        [Column("DiasRenta")]
        public int DiasRenta { get; set; }

        public virtual Libro Libro { get; set; }
        public virtual Usuario Usuario { get; set;}
    }
}
