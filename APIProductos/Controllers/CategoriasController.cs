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

        //public IActionResult Get()
        //{

        //}
    }
}
