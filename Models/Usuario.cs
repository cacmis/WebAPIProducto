using System;

namespace MiPrimerWebApi.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Correo { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Nombre { get; set; }  
        public string Telefono { get; set; }
        public DateTime FechaCreacion { get; set; }
        public bool Activo { get; set; }
    }
}