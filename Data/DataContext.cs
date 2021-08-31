using Microsoft.EntityFrameworkCore;
using MiPrimerWebApi.Models;
namespace MiPrimerWebApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {
            
        }

        public DbSet<Usuario> Usuarios  { get; set; }
        public DbSet<Producto> Productos { get; set; }
    }
}