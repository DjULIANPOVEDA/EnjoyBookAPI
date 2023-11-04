using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnjoyBookAPI.Models;

[Table("usuario")]
public class Usuario
{
    [Key, Column("id")]
    public string Id { get; set; } = null!;
    [Column("rol")]
    public string Rol { get; set; } = null!;
    [Column("username")]
    public string Username { get; set; } = null!;
    [Column("password")]
    public string Password { get; set; } = null!;
    [Column("telefono")]
    public string Telefono { get; set; } = null!;
    [Column("correo")]
    public string Correo { get; set; } = null!;
    [Column("direccion")]
    public string Direccion { get; set; } = null!;

    public virtual ICollection<Libro> Libros { get; set; }
    public virtual ICollection<Renta> Rentas { get; set; }
}
