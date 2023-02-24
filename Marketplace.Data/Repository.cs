using Marketplace.Core.Interfaces;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace Marketplace.Data;

public class Repository<T> : IRepository<T> where T : class
{
    private protected readonly MarketplaceContext _context;

    public Repository(MarketplaceContext context) => _context = context;

    public T? Get(int id) => _context.Set<T>().Find(id);

    public IEnumerable<T> GetAll() => _context.Set<T>();

    public void Add(T entity) => _context.Set<T>().Add(entity);

    public void AddRange(IEnumerable<T> entities) => _context.Set<T>().AddRange(entities);

    public IEnumerable<T> Find(Expression<Func<T, bool>> expression) => 
        _context.Set<T>().Where(expression);

    public void Update(T entity) => _context.Set<T>().Update(entity);

    public void Remove(T entity) => _context.Set<T>().Remove(entity);

    public int Count() => _context.Set<T>().Count();

    public EntityEntry<T> Entry(T entity) => _context.Entry(entity);
}