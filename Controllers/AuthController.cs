using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MiPrimerWebApi.Data.Interfaces;
using MiPrimerWebApi.Dtos;
using MiPrimerWebApi.Models;
using MiPrimerWebApi.Services.Interfaces;

namespace MiPrimerWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AuthController(IMapper mapper, IAuthRepository repo, ITokenService tokenService)
        {
            _mapper = mapper;
            _repo = repo;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UsuarioRegisterDto usuarioDto)
        {
            // validate request

            usuarioDto.Correo = usuarioDto.Correo.ToLower();

            if (await _repo.UserExists(usuarioDto.Correo))
                return BadRequest("Usuario con ese correo ya existe");


            var usuarioToCreate = _mapper.Map<Usuario>(usuarioDto);

            var UsuarioCreated = await _repo.Register(usuarioToCreate, usuarioDto.Password);

            var UsuarioReturn = _mapper.Map<UsuarioListDto>(UsuarioCreated);

            return Ok(UsuarioReturn);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UsuarioLoginDto usuarioLoginDto)
        {
            var usuarioFromRepo = await _repo.Login(usuarioLoginDto.Correo.ToLower(), usuarioLoginDto.Password);

            if (usuarioFromRepo == null)
                return Unauthorized();


            var usuario = _mapper.Map<UsuarioListDto>(usuarioFromRepo);

            return Ok(new
            {
                token = _tokenService.CreateToken(usuarioFromRepo),
                usuario
            });
        }
    }
}