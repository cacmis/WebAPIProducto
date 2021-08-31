using System;

namespace MiPrimerWebApi.Dtos
{
    public class UsuarioListDto
    {
        public int Id { get; set; }
        public string Correo { get; set; }
        public string Nombre { get; set; }  
        public string Telefono { get; set; }
        public DateTime FechaCreacion { get; set; }
        public bool Activo { get; set; }
    }
}