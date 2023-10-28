using APIProductos.Models.DTOs;
using APIProductos.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIProductos.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductosController : ControllerBase
	{
		private readonly ProductosRepository productosRepository;
		private readonly CategoriasRepository categoriasRepository;

		public ProductosController(ProductosRepository productosRepository, CategoriasRepository categoriasRepository)
		{
			this.productosRepository = productosRepository;
			this.categoriasRepository = categoriasRepository;
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
	}

}
