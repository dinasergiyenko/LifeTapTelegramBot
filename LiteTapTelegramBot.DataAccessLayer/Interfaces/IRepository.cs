using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace LiteTapTelegramBot.DataAccessLayer.Interfaces
{
    public interface IRepository<TEntity, TKey> where TEntity : class
    {
        void Add(TEntity entity);
        
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        void Remove(TKey id);
    }
}