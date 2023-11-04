using System.ComponentModel.DataAnnotations.Schema;

namespace EnjoyBookAPI.Models.Request
{
    public class RegisterRequest
    {
        public string Id { get; set; } = null!;
        public Roles Rol { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string Direccion { get; set; } = null!;
    }
}
