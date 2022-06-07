using System.Linq;
using Akgunler.Models;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Akgunler.Data
{
    public interface IRepositoryWithTypedId<T, in TId> where T : IEntityWithTypedId<TId>
    {
        DbContext GetContext();

        IQueryable<T> Query();

        IQueryable<T> GetQueryable(Expression<Func<T, bool>> filter = null);

        void Add(T entity);

        void AddRange(T[] entities);

        void Update(T entity);

        void UpdateRange(T[] entities);

        T UpdateCurrentValues(T oldEntity, T newEntity);

        IDbContextTransaction BeginTransaction();

        void SaveChange();

        Task SaveChangeAsync();

        void Remove(T entity);

        void RemoveRange(IEnumerable<T> entity);
    }
}
