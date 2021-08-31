using System;
using System.ComponentModel.DataAnnotations;

namespace MiPrimerWebApi.Dtos
{
    public class UsuarioRegisterDto
    {
         [Required]
         public string Correo { get; set; }
        [Required]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "El password debe tener entre 4 y 8 characteres")]
        public string Password { get; set; }
        [Required]
        public string Nombre { get; set; }  
        [Required]
        public string Telefono { get; set; }
        public DateTime FechaCreacion { get; set; }
        public bool Activo { get; set; }

        public UsuarioRegisterDto()
        {
            FechaCreacion= DateTime.Now;
            Activo = true;
        }
    }
}