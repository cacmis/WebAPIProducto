using MiPrimerWebApi.Models;

namespace MiPrimerWebApi.Services.Interfaces
{
    public interface ITokenService
    {
         string CreateToken(Usuario usuario);
    }
}