namespace APIProductos.Models.DTOs
{

	public class ProductoDTO
	{
		public int Id { get; set; }
		public string Nombre { get; set; } = null!;
		public double? Precio { get; set; }
		public int? IdCategoria { get; set; }	
	}
}
