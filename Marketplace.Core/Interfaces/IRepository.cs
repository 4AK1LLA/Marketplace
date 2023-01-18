using System.Linq.Expressions;

namespace Marketplace.Core.Interfaces;

public interface IRepository<T> where T : class
{
    public T? Get(int id);

    public IEnumerable<T> GetAll();

    public void Add(T entity);

    public IEnumerable<T> Find(Expression<Func<T, bool>> expression);

    public void Update(T entity);

    public void Remove(T entity);
}