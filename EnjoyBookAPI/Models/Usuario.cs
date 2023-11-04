using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnjoyBookAPI.Models;

[Table("usuario")]
public class Usuario
{
    [Column("id")]
    public string Id { get; set; } = null!;
    [Column("rol")]
    public Roles Rol { get; set; }
    [Column("userName")]
    public string Username { get; set; } = null!;
    [Column("password")]
    public string Password { get; set; } = null!;
    [Column("telefono")]
    public string Telefono { get; set; } = null!;
    [Column("correo")]
    public string Correo { get; set; } = null!;
    [Column("direccion")]
    public string Direccion { get; set; } = null!;
}
