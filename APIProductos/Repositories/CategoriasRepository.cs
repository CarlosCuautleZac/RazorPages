using APIProductos.Models.Entities;

namespace APIProductos.Repositories
{
    public class CategoriasRepository : Repository<Categorias>
    {
        public CategoriasRepository(ItesrcneIntegracionContext context) : base(context)
        {
        }

        public override IEnumerable<Categorias> GetAll()
        {
            return base.GetAll().OrderBy(x => x.Nombre);
        }

        public Categorias? GetByName(string nombre)
        {
            return base.GetAll().FirstOrDefault(x => x.Nombre == nombre);
        }

        public override void Delete(Categorias entity)
        {
            if (Context.Productos2.Any(x => x.IdCategoria == entity.Id))
            {
                throw new InvalidOperationException("no se puede eliminar una categoria que tiene productos");
            }
            base.Delete(entity);
        }
    }
}
