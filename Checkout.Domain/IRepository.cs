using System.Collections.Generic;

namespace Checkout.Domain
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        void Add(TEntity entity);
        void Delete(TEntity entity);
        TEntity Get(string id);
        IEnumerable<TEntity> GetAll();
    }
}
