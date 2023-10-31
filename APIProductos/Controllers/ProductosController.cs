using APIProductos.Models.DTOs;
using APIProductos.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace APIProductos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly ProductosRepository productosRepository;
        private readonly CategoriasRepository categoriasRepository;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ProductosController(ProductosRepository productosRepository, CategoriasRepository categoriasRepository, IWebHostEnvironment webHostEnvironment)
        {
            this.productosRepository = productosRepository;
            this.categoriasRepository = categoriasRepository;
            this.webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var datos = productosRepository.GetAll().GroupBy(x => x.IdCategoriaNavigation).Select(x => new CategoriaDTO
            {
                Id = x.Key.Id,
                Nombre = x.Key.Nombre,
                Productos = x.Select(p => new ProductoDTO
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Precio = p.Precio,
                }).OrderBy(x => x.Nombre).ToList()
            });

            return Ok(datos);

        }

        [HttpPost]
        public IActionResult Post(ProductoDTO p)
        {
            if (string.IsNullOrWhiteSpace(p.Nombre))
                return BadRequest("Indique el nombre del producto.");
            if (p.Precio != null && p.Precio <= 0)
                return BadRequest("El precio del producto debe ser mayor a cero.");
            if (p.IdCategoria == null)
                return BadRequest("Indique la categoria del producto");
            if (productosRepository.Get(p.Nombre) != null)
                return BadRequest("Ya existe un producto registrado con el mismo nombre");
            if (categoriasRepository.Get(p.IdCategoria) == null)
                return BadRequest("No existe la categoria.");

            var producto = new Models.Entities.Productos2
            {
                Nombre = p.Nombre,
                Precio = p.Precio,
                IdCategoria = p.IdCategoria ?? 0

            };

            productosRepository.Insert(producto);

            return Ok(p.Id);
        }

        [HttpPost("Imagen")]
        public IActionResult Post(ImagenDTO dto)
        {
            //Validar
            if (string.IsNullOrWhiteSpace(dto.ImagenBase64))
                return BadRequest("No se incluyo la imagen del producto");

            var producto = productosRepository.Get(dto.Id);

            if (producto == null)
            {
                return BadRequest("No existe el producto especificado");
            }

            var ruta = webHostEnvironment.WebRootPath + "/productos/";
            System.IO.Directory.CreateDirectory(ruta);

            ruta += dto.Id + ".jpg";

            var file = System.IO.File.Create(ruta);

            byte[] image = Convert.FromBase64String(dto.ImagenBase64);
            file.Write(image, 0, image.Length);
            file.Close();

            return Ok();
        }


        //con esto bajas el archivo con un endpoint--- mas seguridad
        [HttpGet("miimagen")]
        public IActionResult Image()
        {
            var ruta = webHostEnvironment.WebRootPath + "/productos/111.jpg";
            byte[] contenido = System.IO.File.ReadAllBytes(ruta);

            return File(contenido, "image/jps", "imagencita.jpg");
        }

        [HttpPut]
        public IActionResult Put(ProductoDTO p)
        {
            if (string.IsNullOrWhiteSpace(p.Nombre))
                return BadRequest("Indique el nombre del producto.");
            if (p.Precio != null && p.Precio <= 0)
                return BadRequest("El precio del producto debe ser mayor a cero.");
            if (p.IdCategoria == null)
                return BadRequest("Indique la categoria del producto");

            var producto = productosRepository.Get(p.Nombre);
            if (producto != null && producto.Id != p.Id)
                return BadRequest("Ya existe un producto registrado con el mismo nombre");
            if (categoriasRepository.Get(p.IdCategoria) == null)
                return BadRequest("No existe la categoria.");


            producto = productosRepository.Get(p.Id);

            if (producto == null)
            {
                return BadRequest("No existe el producto");
            }
            else
            {
                producto.Precio = p.Precio;
                producto.Nombre = p.Nombre;
                producto.IdCategoria = p.IdCategoria ?? 0;

                productosRepository.Update(producto);

                return Ok();

            }

        }


        [HttpDelete]
        public IActionResult Delete(ProductoDTO p)
        {
            var producto = productosRepository.Get(p.Id);
            if (producto == null)
                return BadRequest("No existe o ya se eliminado");

            else
            {
                productosRepository.Delete(producto);

                //borro la imagen
                var ruta = webHostEnvironment.WebRootPath + $"/productos/{p.Id}.jpg";
                System.IO.File.Delete(ruta);    

                return Ok();
            }


        }
    }

}
