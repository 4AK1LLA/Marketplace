using Marketplace.Core.Interfaces;
using System.Linq.Expressions;

namespace Marketplace.Data;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly MarketplaceContext _context;

    public Repository(MarketplaceContext context) => _context = context;

    public T? Get(int id) => _context.Set<T>().Find(id);

    public IEnumerable<T> GetAll() => _context.Set<T>().ToList();

    public void Add(T entity) => _context.Set<T>().Add(entity);

    public IEnumerable<T> Find(Expression<Func<T, bool>> expression) => 
        _context.Set<T>().Where(expression);

    public void Update(T entity) => _context.Set<T>().Update(entity);

    public void Remove(T entity) => _context.Set<T>().Remove(entity);
}