using System;

namespace MiPrimerWebApi.Dtos
{
    public class ProductoCreateDto
    {
        public string Nombre { get; set; }
        public string  Descripcion { get; set; }
        public decimal Precio { get; set; }

        public DateTime FechaAlta { get; set; }
        public bool Activo { get; set; }

        public ProductoCreateDto()
        {
            FechaAlta= DateTime.Now;
            Activo= true;
        }
    }
}