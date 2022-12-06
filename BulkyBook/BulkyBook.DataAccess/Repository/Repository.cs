using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BulkyBook.DataAccess.Repository;
public class Repository<T> : IRepository<T> where T : class
{
    private readonly ApplicationDbContext _applicationDbContext;
    internal DbSet<T> _dbSet;
    public Repository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
        _dbSet = _applicationDbContext.Set<T>();
    }

    public void Add(T entity) => _dbSet.Add(entity);

    public IEnumerable<T> GetAll(string? includeProperties = null)
    {
        var query = GetQueryWithIncludeObjects(includeProperties, _dbSet);
        return query.ToList();
    }

    private static IQueryable<T> GetQueryWithIncludeObjects(string? includeProperties, DbSet<T> dbSet)
    {
        IQueryable<T> query = dbSet;

        if (string.IsNullOrWhiteSpace(includeProperties))
            return query;
        
        foreach (var includeProp in includeProperties.Split(",", StringSplitOptions.RemoveEmptyEntries))
            query = query.Include(includeProp);

        return query;
    }

    public T GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProperties = null)
    {
        var query = GetQueryWithIncludeObjects(includeProperties, _dbSet);
        query = query.Where(filter);
        return query.FirstOrDefault();
    }

    public void Remove(T entity) => _dbSet.Remove(entity);

    public void RemoveRange(IEnumerable<T> entities) => _dbSet.RemoveRange(entities);
}
