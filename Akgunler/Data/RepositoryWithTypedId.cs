using Akgunler.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Akgunler.Data
{
	public class RepositoryWithTypedId<T, TId> : IRepositoryWithTypedId<T, TId> where T : class, IEntityWithTypedId<TId>
    {
        public RepositoryWithTypedId(AkgunlerContext context)
        {
            Context = context;
            DbSet = Context.Set<T>();
        }

        protected DbContext Context { get; }

        protected DbSet<T> DbSet { get; }

        public DbContext GetContext() => Context;

        public void Add(T entity)
        {
            DbSet.Add(entity);
        }

        public void AddRange(T[] entities)
        {
            DbSet.AddRange(entities);
        }

        public void Update(T entity) 
        {
            var local = Context.Set<T>()
                .Local
                .FirstOrDefault(entry => entry.Id.Equals(entity.Id));
            if (local != null)
            {
                Context.Entry(local).State = EntityState.Detached;
            }
            Context.Entry<T>(entity).State = EntityState.Modified;
			Context.SaveChanges();
		}

        public void UpdateRange(T[] entities)
        {
            foreach (var entity in entities)
            {
                var local = Context.Set<T>()
                    .Local
                    .FirstOrDefault(entry => entry.Id.Equals(entity.Id));
                if (local != null)
                {
                    Context.Entry(local).State = EntityState.Detached;
                }
                Context.Entry<T>(entity).State = EntityState.Modified;
            }
            
            Context.SaveChanges();
        }

        public T UpdateCurrentValues(T oldEntity, T newEntity)
        {

            var attachedEntry = Context.Entry(oldEntity);
            attachedEntry.CurrentValues.SetValues(newEntity);
            Context.SaveChanges();
                return newEntity;
            
        }

        public IDbContextTransaction BeginTransaction()
        {
            return Context.Database.BeginTransaction();
        }

        public void SaveChange()
        {
            Context.SaveChanges();
        }

        public async Task SaveChangeAsync()
        {
            await Context.SaveChangesAsync();
        }

        public IQueryable<T> Query()
        {
            return DbSet;
        }

        public IQueryable<T> GetQueryable(Expression<Func<T, bool>> filter = null)
        {
            return filter == null
                ? Context.Set<T>().AsQueryable()
                : Context.Set<T>().Where(filter).AsQueryable();            
        }

        public void Remove(T entity)
        {
            var local = Context.Set<T>()
               .Local
               .FirstOrDefault(entry => entry.Id.Equals(entity.Id));
            if (local != null)
            {
                Context.Entry(local).State = EntityState.Detached;
            }
            DbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            //foreach (var item in entity)
            //{
            //    var local = Context.Set<T>()
            //   .Local
            //   .FirstOrDefault(entry => entry.Id.Equals(item.Id));
            //    if (local != null)
            //    {
            //        Context.Entry(local).State = EntityState.Detached;
            //    }
            //    DbSet.Remove(item);
            //}
            Context.RemoveRange(entity);
            //Context.SaveChanges();
        }
    }
}
