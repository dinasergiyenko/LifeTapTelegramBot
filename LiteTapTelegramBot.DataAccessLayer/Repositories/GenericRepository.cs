using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LiteTapTelegramBot.DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LiteTapTelegramBot.DataAccessLayer.Repositories
{
    public class GenericRepository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class
    {
        protected readonly DbSet<TEntity> DbSet;

        public GenericRepository(
            DatabaseContext db)
        {
            DbSet = db.Set<TEntity>();
        }

        public virtual void Add(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet
                .Where(predicate)
                .AsEnumerable();
        }

        public virtual void Remove(TKey id)
        {
            var entity = DbSet.Find(id);
            DbSet.Remove(entity);
        }
    }
}