﻿using System.Linq.Expressions;

namespace BulkyBook.DataAccess.Repository.IRepository;
public interface IRepository<T> where T : class
{
    T GetFirstOrDefault(Expression<Func<T, bool>> filter);
    T GetFirstOrDefault(Expression<Func<T, bool>> filter, string includeProperties);
    IEnumerable<T> GetAll();
    IEnumerable<T> GetAll(string includeProperties);
    void Add(T entity);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);
}
