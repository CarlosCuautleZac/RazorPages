using APIProductos.Models.DTOs;
using APIProductos.Models.Entities;
using APIProductos.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIProductos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly CategoriasRepository repository;

        public CategoriasController(CategoriasRepository repository)
        {
            this.repository = repository;
        }

        public IActionResult Get()
        {
            var categorias = repository.GetAll().Select(x => new CategoriaDTO { Id = x.Id, Nombre = x.Nombre });
            return Ok(categorias);
        }

        [HttpPost]
        public IActionResult Post(CategoriaDTO dto)
        {
            //Validamos
            if (ModelState.IsValid)
            {
                Categorias c = new()
                {
                    Nombre = dto.Nombre
                };

                repository.Insert(c);


            }
            return Ok();
        }

        [HttpPut]
        public IActionResult Put(CategoriaDTO dto)
        {
            //Validamos
            if (ModelState.IsValid)
            {
                var categoria = repository.Get(dto.Id);

                if (categoria != null)
                {
                    categoria.Nombre = dto.Nombre;
                    repository.Update(categoria);

                }
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var categoria = repository.Get(id);
                if (categoria != null)
                {
                    repository.Delete(categoria);
                }
                else
                {
                    return NotFound();
                }
                return Ok();

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }
    }
}
