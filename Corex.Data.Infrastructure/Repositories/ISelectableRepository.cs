using Corex.Model.Derived.EntityModel;
using Corex.Model.Infrastructure;
using PagedList.Core;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Corex.Data.Infrastructure
{
    public interface ISelectableRepository<TEntityModel, TKey> :
      IRepository<TEntityModel, TKey> where TEntityModel : class, IEntityModel<TKey>
    {
        IQueryable<TEntityModel> GetList();
        IQueryable<TEntityModel> GetList(Expression<Func<TEntityModel, bool>> predicate);
        IPagedList<TEntityModel> GetList(IPagerInputModel pagerInputModel);
        TEntityModel Get(Expression<Func<TEntityModel, bool>> predicate);
        bool Contains(Expression<Func<TEntityModel, bool>> predicate);
        int Count(Expression<Func<TEntityModel, bool>> predicate);
         
    }
}
