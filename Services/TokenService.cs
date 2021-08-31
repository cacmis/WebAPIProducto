
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MiPrimerWebApi.Models;
using MiPrimerWebApi.Services.Interfaces;

namespace MiPrimerWebApi.Services
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;

        public TokenService(IConfiguration config )
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Token"]));
        }

        string ITokenService.CreateToken(Usuario usuario)
        {
          var claims = new List<Claim>
          {
              new Claim(JwtRegisteredClaimNames.NameId, usuario.Correo)
          };

          var credemtials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
        
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(2),
                SigningCredentials = credemtials
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

        }
    }
}