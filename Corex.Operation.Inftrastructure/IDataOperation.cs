using Corex.Model.Derived.EntityModel;
using Corex.Model.Infrastructure;
using PagedList.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Corex.Operation.Infrastructure
{
    public interface IDataOperation<TEntity, TKey> : ISingletonDependecy
        where TEntity : IEntityModel<TKey>
    {
        TEntity Get(Expression<Func<TEntity, bool>> predicate);
        IPagedList<TEntity> GetPagedList<TPargerInputModel>(IPagerInputModel pagerInputModel);
        IQueryable<TEntity> GetList(Expression<Func<TEntity, bool>> predicate);
        List<TEntity> GetList();
        TEntity Insert(TEntity entity);
        TEntity Update(TEntity entity);
        bool Delete(TEntity entity);
        bool Delete(TKey id);
        int Count(Expression<Func<TEntity, bool>> predicate);
        bool Contains(Expression<Func<TEntity, bool>> predicate);
    }
}
