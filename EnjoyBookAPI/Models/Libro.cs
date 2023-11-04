using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnjoyBookAPI.Models;

[Table("libro")]
public class Libro
{
    [Key, Column("id")]
    public string Id { get; set; } = null!;
    [Column("propietario")]
    public string UsuarioId { get; set; } = null!;
    [Column("nombre")]
    public string Nombre { get; set; } = null!;
    [Column("autor")]
    public string Autor { get; set; } = null!;
    [Column("editor")]
    public string Editor { get; set; } = null!;
    [Column("npag")]
    public int Npag { get; set; }
    [Column("estado")]
    public string Estado { get; set; } = null!;
    [Column("esta_vendido")]
    public bool EstaVendido { get; set; }
    [Column("esta_rentado")]
    public bool EstaRentado { get; set; }
    [Column("precio_venta")]
    public decimal PrecioVenta { get; set; }
    [Column("precio_renta_dia")]
    public decimal PrecioRentaDia { get; set; }

    public virtual Usuario Usuario { get; set; }
    public virtual ICollection<Renta> Rentas { get; set; }
}
