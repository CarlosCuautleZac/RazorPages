using APIProductos.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace APIProductos.Repositories
{
	public class ProductosRepository : Repository<Productos2>
	{
		private readonly ItesrcneIntegracionContext context;

		public ProductosRepository(ItesrcneIntegracionContext context):base(context)  
        {
			this.context = context;
		}

		public override IEnumerable<Productos2> GetAll()
		{
			return context.Productos2.Include(x=> x.IdCategoriaNavigation).OrderBy(x=>x.Nombre);
		}

		public Productos2? Get(string nombre)
		{
			return context.Productos2.FirstOrDefault(x => x.Nombre == nombre);
		}

		public override void Delete(Productos2 entity)
		{
			//Aqui elimino los relacionados
			var existe = context.Listacompras.Where(x => x.IdProducto == entity.Id);
			context.RemoveRange(existe);
			context.SaveChanges();

			base.Delete(entity);
		}
	}
}
