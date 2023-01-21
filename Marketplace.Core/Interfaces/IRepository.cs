using System.Linq.Expressions;

namespace Marketplace.Core.Interfaces;

public interface IRepository<T> where T : class
{
    T? Get(int id);

    IEnumerable<T> GetAll();

    void Add(T entity);

    void AddRange(IEnumerable<T> entities);

    IEnumerable<T> Find(Expression<Func<T, bool>> expression);

    void Update(T entity);

    void Remove(T entity);

    int Count();
}