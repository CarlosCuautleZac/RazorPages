using APIProductos.Models.Entities;

namespace APIProductos.Repositories
{
    public class Repository<T> where T : class
    {
        public Repository(ItesrcneIntegracionContext context)
        {

            Context = context;

        }

        public ItesrcneIntegracionContext Context { get; set; }

        public virtual IEnumerable<T> GetAll()
        {
            return Context.Set<T>();
        }

        public virtual T? Get(object id)
        {
            return Context.Find<T>(id);
        }

        public virtual void Insert(T entity)
        {

            Context.Add(entity); Context.SaveChanges();
        }

        public virtual void Update(T entity)
        {
            Context.Update(entity);
            Context.SaveChanges();
        }

        public virtual void Delete(object id)
        {
            Context.Remove(id); Context.SaveChanges();
        }
    }
}
