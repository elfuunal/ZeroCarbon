﻿using NeyeTech.ZeroCarbon.Core.Entities;
using System.Linq.Expressions;

namespace NeyeTech.ZeroCarbon.Core.DataAccess
{
    public interface IEntityRepository<T>
        where T : class, IEntity
    {
        T Add(T entity);
        T Update(T entity);
        void Delete(T entity);
        IEnumerable<T> GetList(Expression<Func<T, bool>> expression = null);
        Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>> expression = null);
        T Get(Expression<Func<T, bool>> expression);
        Task<T> GetAsync(Expression<Func<T, bool>> expression);
        int SaveChanges();
        Task<int> SaveChangesAsync();
        IQueryable<T> Query();
    }
}
