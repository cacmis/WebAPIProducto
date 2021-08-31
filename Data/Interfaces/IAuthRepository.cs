using System.Threading.Tasks;
using MiPrimerWebApi.Models;

namespace MiPrimerWebApi.Data.Interfaces
{
    public interface IAuthRepository
    {
        Task<Usuario> Register(Usuario usuario, string password);
        Task<Usuario> Login(string correo, string password);
        Task<bool> UserExists(string Correo);
    }
}