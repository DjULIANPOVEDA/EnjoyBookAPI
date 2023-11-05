using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnjoyBookAPI.Models
{
    [Table("renta")]
    public class Renta
    {
        [Key, Column("id")]
        public string Id { get; set; } = null!;
        [Column("idlibro")]
        public string LibroId { get; set; } = null!;
        [Column("idusuario")]
        public string UsuarioId { get; set; } = null!;
        [Column("fecharenta")]
        public string FechaRenta { get; set; }
        [Column("diasrenta")]
        public int DiasRenta { get; set; }

        public virtual Libro Libro { get; set; }
        public virtual Usuario Usuario { get; set;}
    }
}
