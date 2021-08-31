using System.Threading.Tasks;
using MiPrimerWebApi.Data.Interfaces;
using MiPrimerWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace MiPrimerWebApi.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;

        public AuthRepository(DataContext context)
        {
            this._context = context;
        }

        public async Task<Usuario> Login(string correo, string password)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(x=> x.Correo == correo);
            if( usuario == null)
                return null;
            if(!VerifyPasswordHash(password,usuario.PasswordHash,usuario.PasswordSalt))
                return null;

            return usuario;
        }


        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                for (int i=0; i< computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i]) return false;
                }
            }
            return true;
        }
        public async  Task<Usuario> Register(Usuario usuario, string password)
        {
            byte[] passwordHash;
            byte[] passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);
            usuario.PasswordHash = passwordHash;
            usuario.PasswordSalt = passwordSalt;

            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();

            return usuario;

        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        public async  Task<bool> UserExists(string Correo)
        {
            if ( await _context.Usuarios.AnyAsync(x=> x.Correo == Correo))
            return true;

            return false;
        }
    }
}