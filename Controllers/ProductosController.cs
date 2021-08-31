using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiPrimerWebApi.Data.Interfaces;
using MiPrimerWebApi.Dtos;
using MiPrimerWebApi.Models;

namespace MiPrimerWebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly IApiRepository _repo;
        private readonly IMapper _mapper;

        public ProductosController(IApiRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var productos = await _repo.GetProductosAsync();

            return Ok(productos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var producto = await _repo.GetProductoByIdAsync(id);

            return Ok(producto);
        }
        
        [HttpGet("nombre/{nombre}")]
        public async Task<IActionResult> Get(string nombre)
        {
            var producto = await _repo.GetProductoByNombreAsync(nombre);

            return Ok(producto);
        }
        
      
        [HttpPost]
        public async Task<IActionResult> Post(ProductoCreateDto productoDto)
        {
             
            var productoToCreate =  _mapper.Map<Producto>(productoDto);
            //var productoToCreate = new Producto();
            // productoToCreate.Nombre = productoDto.Nombre;
            // productoToCreate.Descripcion = productoDto.Descripcion;
            // productoToCreate.Descripcion = productoDto.Descripcion;
            // productoToCreate.Precio = productoDto.Precio;

             _repo.Add(productoToCreate);
             if(await _repo.SaveAll())
                return Ok(productoToCreate);
            
            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put (int id, ProductoUpdateDto productoDto)
        {
            if(id != productoDto.Id)
                return BadRequest("Los Ids no coinciden");

            var productoToUpdate= await _repo.GetProductoByIdAsync(productoDto.Id);

            if(productoToUpdate == null)
                return BadRequest();

             _mapper.Map(productoDto,productoToUpdate);
            // puestoToUpdate.Descripcion = productoDto.Descripcion;
            // puestoToUpdate.Precio = productoDto.Precio;
            //_repo.Update(puestoToUpdate);
            if(!await _repo.SaveAll())
                return NoContent();

            return Ok(productoToUpdate); 


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var producto = await _repo.GetProductoByIdAsync(id);
            if(producto == null)
                return NotFound("Producto no encontrado");

            _repo.Delete(producto);
            if(!await _repo.SaveAll())
                return NoContent();

            return Ok("Producto borrado");
        }
    }

    
}