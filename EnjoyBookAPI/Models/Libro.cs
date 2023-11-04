using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnjoyBookAPI.Models;

[Table("libro")]
public class Libro
{
    [Column("id")]
    public string Id { get; set; } = null!;
    [Column("nombre")]
    public string Nombre { get; set; } = null!;
    [Column("autor")]
    public string Autor { get; set; } = null!;
    [Column("editor")]
    public string Editor { get; set; } = null!;
    [Column("npag")]
    public int Npag { get; set; }
    [Column("estado")]
    public Estados Estado { get; set; }
    [Column("precio")]
    public decimal Precio { get; set; }
}
